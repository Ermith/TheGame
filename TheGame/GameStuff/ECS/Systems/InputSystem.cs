using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.Math;

namespace TheGame.GameStuff.ECS.Systems
{
  class InputSystem : System
  {

    private List<Entity> inputEntities;
    public InputSystem(List<Entity> inputEntities)
    {
      this.inputEntities = inputEntities;
    }
    public override void Update(GameTime time)
    {
      MouseState mouse = Mouse.GetState();
      KeyboardState keyboard = Keyboard.GetState();

      foreach (Entity entity in inputEntities)
      {
        CInput input = entity.Get<CInput>();
        CMovement movement = entity.Get<CMovement>();
        CSpacial location = entity.Get<CSpacial>();
        CAnimation animation = entity.Get<CAnimation>();

        Vector2 Velocity = new Vector2(0, 0);

        if (keyboard.IsKeyDown(Keys.G))
          animation.State = AnimtaionState.Inactive;


        if (keyboard.IsKeyDown(input.Up))
        {
          Velocity += CommonVectors.Up;
          location.Facing = Direction.Up;
        }
        if (keyboard.IsKeyDown(input.Down))
        {
          Velocity += CommonVectors.Down;
          location.Facing = Direction.Down;
        }
        if (keyboard.IsKeyDown(input.Left))
        {
          Velocity += CommonVectors.Left;
          location.Facing = Direction.Left;
        }
        if (keyboard.IsKeyDown(input.Right))
        {
          Velocity += CommonVectors.Right;
          location.Facing = Direction.Right;
        }

        animation.Row = (int)location.Facing;

        if (Velocity.Length() != 0)
        {
          Velocity.Normalize();
          if (animation.State == AnimtaionState.Stopped)
            animation.State = AnimtaionState.Starting;
        }
        else if (animation.State == AnimtaionState.Starting || animation.State == AnimtaionState.Playing)
        {
          animation.State = AnimtaionState.Ending;
          animation.AnimationTime = 0;
        }


        movement.Velocity = Velocity * movement.Speed * (float)time.ElapsedGameTime.TotalMilliseconds;
      }
    }
  }
}
