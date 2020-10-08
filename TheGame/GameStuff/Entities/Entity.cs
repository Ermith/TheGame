using Microsoft.Xna.Framework;
using System;


namespace TheGame.GameStuff.Entities {
  abstract class Entity : IGameComponent {
    public Action Action { get; private set; } = null;
    public Point Position;

    public abstract void Render(RenderArguments arguments);

    public abstract void Update(UpdateArguments arguments);
  }
}
