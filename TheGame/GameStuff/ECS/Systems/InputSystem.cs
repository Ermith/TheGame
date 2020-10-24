using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;
using TheGame.Math;

namespace TheGame.GameStuff.ECS.Systems
{
  class InputSystem : System
  {
    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in CInput.entities)
      {
        CInput input = entity.Components[ComponentTypes.Input] as CInput;
        CMovement movement = entity.Components[ComponentTypes.Movement] as CMovement;
        CSpacial location = entity.Components[ComponentTypes.Spacial] as CSpacial;

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
