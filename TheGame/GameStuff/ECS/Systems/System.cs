
namespace TheGame.GameStuff.ECS.Systems
{
  abstract class System : IUpdatable
  {
    public abstract void Update(UpdateArguments arguments);
  }
}
