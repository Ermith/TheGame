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
      Vector2 newPosition = entity.Position + movement;
      if (entity.World.CheckPosition(
        new Rectangle(
          (int)newPosition.X,
          (int)newPosition.Y,
          entity.Width,
          entity.Height
          )
        ))
        entity.Position += movement;
    }
  }
}
