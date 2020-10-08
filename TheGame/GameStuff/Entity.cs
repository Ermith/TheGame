

namespace TheGame.GameStuff {
  abstract class Entity : IGameComponent {
    public abstract void Render(RenderArguments arguments);
    public abstract void Update(UpdateArguments arguments);
  }
}
