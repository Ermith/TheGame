using TheGame.GameStuff.ECS;
using System.Collections.Generic;
using TheGame.Math;

namespace TheGame.GameStuff.ECS.Components
{
  class CLocation : Component
  {
    public static List<Entity> entities = new List<Entity>();
    public static void AddLocationComponent(Entity entity)
    {
      entities.Add(entity);
      entity.Components[ComponentTypes.Location] = new CLocation();
    }
    public float X { get; set; }
    public float Y { get; set; }
    public Direction Direction { get; set; }
  }
}
