using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using TheGame.GameStuff.Actions;


namespace TheGame.GameStuff.Entities
{
  abstract class Entity : IGameComponent
  {
    public Entity(World world)
    {
      World = world;
    }
    public Action Action { get; protected set; } = null;
    public Vector2 Position;
    public World World;
    public int Width;
    public int Height;
    public Rectangle Rectangle => new Rectangle(Position.ToPoint(), new Point(Width, Height));

    public abstract void Render(RenderArguments arguments);

    public abstract void Update(UpdateArguments arguments);
  }
}
