namespace TheGame.States
{
  abstract class State : IGameComponent
  {
    public static State GameState { get; set; }
    public static State MenuState { get; set; }
    public static State CurrentState { get; set; }
    public abstract void Update(UpdateArguments arguments);
    public abstract void Render(RenderArguments arguments);
  }
}
