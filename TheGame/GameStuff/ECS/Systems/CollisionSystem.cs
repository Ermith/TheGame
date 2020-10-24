using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class CollisionSystem : System
  {
    public CollisionSystem(World world)
    {
      World = world;
    }

    public World World { get; }

    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in CMovement.entities)
      {
        CMovement movement = entity.Components[ComponentTypes.Movement] as CMovement;
        CSpacial spacial = entity.Components[ComponentTypes.Spacial] as CSpacial;

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
