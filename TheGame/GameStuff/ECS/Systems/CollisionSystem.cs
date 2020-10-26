using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;
using System.Collections.Generic;

namespace TheGame.GameStuff.ECS.Systems
{
  class CollisionSystem : System
  {
    public CollisionSystem(World world, List<Entity> movementEntities)
    {
      World = world;
      this.movementEntities = movementEntities;
    }

    public World World { get; }
    private List<Entity> movementEntities;
    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in movementEntities)
      {
        CMovement movement = entity.Get<CMovement>();
        CSpacial spacial = entity.Get<CSpacial>();

        Vector2 newPosition = spacial.Position;

        // Vertical movement
        if (World.CheckPosition(
          new Rectangle(
            (int)(spacial.Position.X + movement.Velocity.X),
            (int)spacial.Position.Y,
            spacial.Width,
            spacial.Height
            )
          ))
          newPosition.X += movement.Velocity.X;

        // Horizontal movement
        if (World.CheckPosition(
          new Rectangle(
            (int)spacial.Position.X,
            (int)(spacial.Position.Y + movement.Velocity.Y),
            spacial.Width,
            spacial.Height
            )
          ))
          newPosition.Y += movement.Velocity.Y;

        spacial.Position = newPosition;
      }
    }
  }
}
