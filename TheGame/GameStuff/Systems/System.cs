using TheGame.GameStuff.Entities;
using TheGame.GameStuff.Components;

namespace TheGame.GameStuff.Systems
{
  abstract class System : IUpdatable
  {
    public abstract void Update(UpdateArguments arguments);
  }
}
