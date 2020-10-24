using TheGame.GameStuff.ECS;
using System.Collections.Generic;
using TheGame.Math;
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Components
{
  class CSpacial : Component
  {
    public static List<Entity> entities = new List<Entity>();
    public Vector2 Position { get; set; }
    public Direction Facing { get; set; }
    public int Width { get; set; } = 32;
    public int Height { get; set; } = 32;
    public Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    public static void AddLocationComponent(Entity entity)
    {
      entities.Add(entity);
      entity.Components[ComponentTypes.Spacial] = new CSpacial();
    }

  }
}
