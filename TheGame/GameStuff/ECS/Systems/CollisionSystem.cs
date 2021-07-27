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

    private bool CheckEntities(Entity entity)
    {
      Rectangle r = entity.Get<CSpacial>().HitBox;
      Rectangle newR = new Rectangle(r.X + r.Width / 6, r.Y + r.Height / 6, r.Width / 3, r.Height / 3);

      foreach (Entity e in movementEntities)
      {
        if (e == entity)
          continue;

        CSpacial s = e.Get<CSpacial>();
        if (newR.Intersects(s.HitBox))
          return false;
      }
      return true;
    }

    public override void Update(GameTime time)
    {
      foreach (Entity entity in movementEntities)
      {
        CMovement movement = entity.Get<CMovement>();
        CSpacial spacial = entity.Get<CSpacial>();

        // Vertical movement
        spacial.X += movement.Velocity.X;
        if (!World.CheckPosition(spacial.HitBox) || !CheckEntities(entity))
          spacial.X -= movement.Velocity.X;

        // Horizontal movement
        spacial.Y += movement.Velocity.Y;
        if (!World.CheckPosition(spacial.HitBox) || !CheckEntities(entity))
          spacial.Y -= movement.Velocity.Y;
      }
    }
  }
}
