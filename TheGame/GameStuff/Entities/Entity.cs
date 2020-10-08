using Microsoft.Xna.Framework;
using TheGame.GameStuff.Actions;


namespace TheGame.GameStuff.Entities {
  abstract class Entity : IGameComponent {
    public Action Action { get; protected set; } = null;
    public Vector2 Position;

    public abstract void Render(RenderArguments arguments);

    public abstract void Update(UpdateArguments arguments);
  }
}
