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
    private KeyboardState lastKeyboard = Keyboard.GetState();
    private Camera camera;
    public InputSystem(List<Entity> inputEntities, Camera camera)
    {
      this.inputEntities = inputEntities;
      this.camera = camera;
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
        var attack = entity.Get<CAttack>();

        switch (behavior.State)
        {
          case State.Attacking:
            HandleAttacking(keyboard, input, behavior, animation, movement, attack, t);
            break;

          case State.Standing:

            HandleStanding(keyboard, input, behavior, animation, movement, t);
            break;

          case State.Moving:

            HandleMoving(keyboard, input, behavior, animation, movement, t);
            break;

          case State.Sneaking:
            HandleSneaking(keyboard, input, behavior, animation, movement, t);
            break;

          case State.Crouching:
            HandleCrouching(keyboard, input, behavior, animation, movement, t);
            break;

          case State.AttackWindup:
            HandleAttackWindup(keyboard, input, behavior, animation, movement, attack, t);
            break;

          case State.AttackFinish:
            HandleAttackFinish(keyboard, input, animation, movement, attack, behavior);
            break;

          default:
            break;
        }
      }

      lastKeyboard = keyboard;
    }

    private void HandleAttackWindup(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, CAttack attack, float time)
    {
      animation.loop = false;
      movement.Velocity = Vector2.Zero;
      if (animation.finished && (!attack.Chargable || !keyboard.IsKeyDown(input.Attack)))
      {
        camera.ShakeEffect(2f);
        EnterState(animation, behavior, State.Attacking, attack.CurrentAttack);
        Assets.Swoosh.Play();
        return;
      }

      if (!animation.finished && !keyboard.IsKeyDown(input.Attack) && attack.Chargable)
      {
        animation.loop = true;
        EnterState(animation, behavior, State.Standing);
        attack.CurrentAttack = 0;
      }
    }
    private void HandleAttacking(KeyboardState keyboard, CInput input, CBehavior behavior, CAnimation animation, CMovement movement, CAttack attack, float time)
    {
      if (animation.finished && attack.attackedEntities.Count != 0)
      {
        EnterState(animation, behavior, State.AttackFinish, attack.CurrentAttack);
        return;
      }

      if (animation.finished && keyboard.IsKeyDown(input.Attack) && attack.CurrentAttack < attack.AttacksCount - 1)
      {
        attack.CurrentAttack++;
        EnterState(animation, behavior, State.AttackWindup, attack.CurrentAttack);
        return;
      }

      if (animation.finished && (attack.attackedEntities.Count == 0 || attack.CurrentAttack == attack.AttacksCount - 1))
      {
        animation.loop = true;
        attack.CurrentAttack = 0;
        EnterState(animation, behavior, State.Standing);
        Stand(movement);
        return;
      }



      Attack();
      return;
    }
    private void HandleAttackFinish(KeyboardState keyboard, CInput input, CAnimation animation, CMovement movement, CAttack attack, CBehavior behavior)
    {
      int i = attack.CurrentAttack;
      /**/
      if (animation.finished && (i == attack.AttacksCount - 1 || !keyboard.IsKeyDown(input.Attack)))
      {
        attack.CurrentAttack = 0;
        animation.loop = true;
        EnterState(animation, behavior, State.Standing);
        Stand(movement);
        return;
      }
      /**/

      if (animation.finished && keyboard.IsKeyDown(input.Attack))
      {
        attack.CurrentAttack++;
        EnterState(animation, behavior, State.AttackWindup, attack.CurrentAttack);
        return;
      }
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

      if (Velocity.Length() != 0)
        Velocity.Normalize();
      movement.Velocity = Velocity * movement.Speed * (float)time;
    }

    private bool IsMoving(KeyboardState keyboard, CInput input)
    {
      return (keyboard.IsKeyDown(input.Up) || keyboard.IsKeyDown(input.Left) || keyboard.IsKeyDown(input.Down) || keyboard.IsKeyDown(input.Right));
    }
    private void EnterState(CAnimation animation, CBehavior behavior, State state, int attackIndex = 0)
    {
      int x, y, count;
      if (CBehavior.AttackStates.Contains(state))
      {
        count = animation.attackFrameCounts[attackIndex][state];
        (x, y) = animation.attackFrameCoords[attackIndex][state];
      } else
      {
        count = animation.frameCounts[state];
        (x, y) = animation.frameCoords[state];
      }

      animation.FrameCount = count;
      animation.X = x;
      animation.Y = y;
      animation.finished = false;
      behavior.State = state;
      animation.Index = 0;
    }
    private void BeginSneak()
    {
      camera.ZoomEffect(-0.1f, 200f);
    }
    private void EndSneak()
    {
      camera.ZoomStop();
    }
  }
}
