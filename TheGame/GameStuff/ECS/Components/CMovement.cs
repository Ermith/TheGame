
using System.Collections.Generic;
using TheGame.GameStuff.ECS;

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
    public float dX { get; set; }
    public float dY { get; set; }
    public float Speed { get; set; } = 0.2f;
  }
}
