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
    public override void Update(GameTime time)
    {
      foreach (Entity entity in movementEntities)
      {
        CMovement movement = entity.Get<CMovement>();
        CSpacial spacial = entity.Get<CSpacial>();

        // Vertical movement
        spacial.X += movement.Velocity.X;
        if (!World.CheckPosition(spacial.HitBox))
          spacial.X -= movement.Velocity.X;

        // Horizontal movement
        spacial.Y += movement.Velocity.Y;
        if (!World.CheckPosition(spacial.HitBox))
          spacial.Y -= movement.Velocity.Y;
      }
    }
  }
}
