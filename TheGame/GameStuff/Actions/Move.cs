using Microsoft.Xna.Framework;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff.Actions
{
  class Move : Action
  {
    private Entity entity;
    private Vector2 movement;

    public Move(Entity entity, Vector2 movement)
    {
      this.entity = entity;
      this.movement = movement;
    }
    public override void Execute()
    {
      Vector2 newPosition = entity.Position;

      // Vertical movement
      if (entity.World.CheckPosition(
        new Rectangle(
          (int)(entity.Position.X + movement.X),
          (int)entity.Position.Y,
          entity.Width,
          entity.Height
          )
        ))
        newPosition.X += movement.X;

      // Horizontal movement
      if (entity.World.CheckPosition(
        new Rectangle(
          (int)entity.Position.X,
          (int)(entity.Position.Y + movement.Y),
          entity.Width,
          entity.Height
          )
        ))
        newPosition.Y += movement.Y;

      entity.Position = newPosition;
    }
  }
}
