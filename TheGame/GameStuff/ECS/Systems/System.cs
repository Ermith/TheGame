
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Systems
{
  abstract class System : IUpdatable
  {
    public abstract void Update(GameTime time);
  }
}
