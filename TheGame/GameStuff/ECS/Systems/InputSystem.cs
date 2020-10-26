using Microsoft.Xna.Framework;
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
    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in inputEntities)
      {
        CInput input = entity.Get<CInput>();
        CMovement movement = entity.Get<CMovement>();
        CSpacial location = entity.Get<CSpacial>();

        Vector2 Velocity = new Vector2(0, 0);

        if (arguments.Keyboard.IsKeyDown(input.Up))
        {
          Velocity += CommonVectors.Up;
          location.Facing = Direction.Up;
        }
        if (arguments.Keyboard.IsKeyDown(input.Down))
        {
          Velocity += CommonVectors.Down;
          location.Facing = Direction.Down;
        }
        if (arguments.Keyboard.IsKeyDown(input.Left))
        {
          Velocity += CommonVectors.Left;
          location.Facing = Direction.Left;
        }
        if (arguments.Keyboard.IsKeyDown(input.Right))
        {
          Velocity += CommonVectors.Right;
          location.Facing = Direction.Right;
        }

        if (Velocity.Length() != 0)
          Velocity.Normalize();

        movement.Velocity = Velocity * movement.Speed * (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;
      }
    }
  }
}
