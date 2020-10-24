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
        CLocation location = entity.Components[ComponentTypes.Location] as CLocation;

        Vector2 Velocity = new Vector2(0, 0);

        if (arguments.Keyboard.IsKeyDown(input.Up))
        {
          Velocity += CommonVectors.Up;
          location.Direction = Direction.Up;
        }
        if (arguments.Keyboard.IsKeyDown(input.Down))
        {
          Velocity += CommonVectors.Down;
          location.Direction = Direction.Down;
        }
        if (arguments.Keyboard.IsKeyDown(input.Left))
        {
          Velocity += CommonVectors.Left;
          location.Direction = Direction.Left;
        }
        if (arguments.Keyboard.IsKeyDown(input.Right))
        {
          Velocity += CommonVectors.Right;
          location.Direction = Direction.Right;
        }

        if (Velocity.Length() != 0)
          Velocity.Normalize();

        movement.dX = Velocity.X * movement.Speed * (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;
        movement.dY = Velocity.Y * movement.Speed * (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;
      }
    }
  }
}
