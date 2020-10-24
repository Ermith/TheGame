using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Components
{
  class CMovement : Component
  {
    public static List<Entity> entities = new List<Entity>();

    public static void AddMovementComponent(Entity entity)
    {
      entities.Add(entity);
      entity.Components[ComponentTypes.Movement] = new CMovement();
    }
    public Vector2 Velocity { get; set; }
    public float Speed { get; set; } = 0.2f;
  }
}
