using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.Math;

namespace TheGame.GameStuff.ECS.Systems
{
  class InputSystem : System
  {
    private List<Entity> inputEntities;
    KeyboardState lastKeyboard = Keyboard.GetState();
    public InputSystem(List<Entity> inputEntities)
    {
      this.inputEntities = inputEntities;
    }
    public override void Update(GameTime time)
    {
      MouseState mouse = Mouse.GetState();
      KeyboardState keyboard = Keyboard.GetState();
      float t = (float)time.ElapsedGameTime.TotalMilliseconds;

      foreach (Entity entity in inputEntities)
      {
        var input = entity.Get<CInput>();
        var movement = entity.Get<CMovement>();
        var behavior = entity.Get<CBehavior>();
        var animation = entity.Get<CAnimation>();

        switch (behavior.State)
        {
          case State.Attacking:
            HandleAttacking(keyboard, input, behavior, animation, movement, t);
            return;

          case State.Standing:

            HandleStanding(keyboard, input, behavior, animation, movement, t);
            return;

          case State.Moving:

            HandleMoving(keyboard, input, behavior, animation, movement, t);
            return;

          case State.Sneaking:
            HandleSneaking(keyboard, input, behavior, animation, movement, t);
            return;

          case State.Crouching:
            HandleCrouching(keyboard, input, behavior, animation, movement, t);
            return;

          case State.AttackWindup:
            HandleAttackWindup(keyboard, input, behavior, animation, movement, t);
            return;

          default:
            break;
        }
      }

      lastKeyboard = keyboard;
    }

    private void HandleAttackWindup(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      animation.loop = false;
      movement.Velocity = Vector2.Zero;
      if (animation.finished && !keyboard.IsKeyDown(input.Attack))
      {
        Camera.ShakeEffect(2f);
        EnterState(animation, behavior, State.Attacking);
        return;
      }

      if (!animation.finished && !keyboard.IsKeyDown(input.Attack))
      {
        animation.loop = true;
        EnterState(animation, behavior, State.Standing);
      }
    }
    private void HandleAttacking(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      if (animation.finished)
      {
        animation.loop = true;
        EnterState(animation, behavior, State.Standing);
        Stand(movement);
        return;

      }

      Attack();
      return;
    }

    private void HandleCrouching(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      if (keyboard.IsKeyDown(input.Attack))
      {
        EndSneak();
        EnterState(animation, behavior, State.AttackWindup);
        return;
      }

      if (!keyboard.IsKeyDown(input.Sneak))
      {
        EndSneak();
        EnterState(animation, behavior, State.Standing);
        Stand(movement);
        return;
      }

      if (IsMoving(keyboard, input))
      {
        EnterState(animation, behavior, State.Sneaking);
        Sneak(animation, movement, input, keyboard, time);
        return;
      }

      Crouch(movement);
      return;
    }

    private void HandleSneaking(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      if (keyboard.IsKeyDown(input.Attack))
      {
        EndSneak();
        EnterState(animation, behavior, State.AttackWindup);
        return;
      }

      if (!IsMoving(keyboard, input))
      {
        EnterState(animation, behavior, State.Crouching);
        Crouch(movement);
        return;
      }

      if (!keyboard.IsKeyDown(input.Sneak))
      {
        EndSneak();
        EnterState(animation, behavior, State.Moving);
        Move(animation, movement, input, keyboard, time);
        return;
      }

      Sneak(animation, movement, input, keyboard, time);
      return;
    }

    private void HandleMoving(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      if (keyboard.IsKeyDown(input.Attack))
      {
        EnterState(animation, behavior, State.AttackWindup);
        return;
      }

      if (keyboard.IsKeyDown(input.Sneak))
      {
        animation.dir = Direction.Up;
        BeginSneak();
        EnterState(animation, behavior, State.Sneaking);
        Sneak(animation, movement, input, keyboard, time);
        return;
      }

      if (IsMoving(keyboard, input))
      {
        Move(animation, movement, input, keyboard, time);
        return;
      }

      EnterState(animation, behavior, State.Standing);
      Stand(movement);
      return;
    }

    private void HandleStanding(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, float time)
    {
      // space -> attack
      if (keyboard.IsKeyDown(input.Attack))
      {
        EnterState(animation, behavior, State.AttackWindup);
        return;
      }

      // wasd -> move
      if (IsMoving(keyboard, input))
      {
        EnterState(animation, behavior, State.Moving);
        Move(animation, movement, input, keyboard, time);
        return;
      }

      // shift -> crouch
      if (keyboard.IsKeyDown(input.Sneak))
      {
        EnterState(animation, behavior, State.Crouching);
        BeginSneak();
        Crouch(movement);
        return;
      }


      Stand(movement);
      return;
    }

    private void Crouch(CMovement movement)
    {
      if (movement.Velocity.Length() < 0.1)
        movement.Velocity = Vector2.Zero;
      else
        movement.Velocity *= 0.9f;
    }

    private void Stand(CMovement movement)
    {
      if (movement.Velocity.Length() < 0.1)
        movement.Velocity = Vector2.Zero;
      else
        movement.Velocity *= 0.9f;
    }

    private void Attack()
    {
    }

    private void Sneak(CAnimation animation, CMovement movement, CInput input, KeyboardState keyboard, float time)
    {
      Move(animation, movement, input, keyboard, time);
      movement.Velocity *= 0.5f;
    }

    private void Move(CAnimation animation, CMovement movement, CInput input, KeyboardState keyboard, float time)
    {
      Vector2 Velocity = new Vector2(0, 0);

      if (keyboard.IsKeyDown(input.Up))
      {
        Velocity += CommonVectors.Up;
        animation.dir = Direction.Up;
      }
      if (keyboard.IsKeyDown(input.Down))
      {
        Velocity += CommonVectors.Down;
        animation.dir = Direction.Down;
      }
      if (keyboard.IsKeyDown(input.Left))
      {
        Velocity += CommonVectors.Left;
        animation.dir = Direction.Left;
      }
      if (keyboard.IsKeyDown(input.Right))
      {
        Velocity += CommonVectors.Right;
        animation.dir = Direction.Right;
      }

      Velocity.Normalize();
      movement.Velocity = Velocity * movement.Speed * (float)time;
    }

    private bool IsMoving(KeyboardState keyboard, CInput input)
    {
      return (keyboard.IsKeyDown(input.Up) || keyboard.IsKeyDown(input.Left) || keyboard.IsKeyDown(input.Down) || keyboard.IsKeyDown(input.Right));
    }
    private void EnterState(CAnimation animation, CBehavior behavior, State state)
    {
      animation.Index = 0;
      animation.FrameCount = animation.frameCounts[state];
      (int x, int y) = animation.frameCoords[state];
      animation.X = x;
      animation.Y = y;
      animation.finished = false;
      behavior.State = state;
    }
    private void BeginSneak()
    {
      Camera.ZoomEffect(-0.3f);
      Camera.ShadowOverlayStart();
    }
    private void EndSneak()
    {
      Camera.ZoomStop();
      Camera.ShadowOverlayStop();
    }
  }
}
