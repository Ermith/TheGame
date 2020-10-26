using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheGame.States
{
  abstract class State : IGameComponent
  {
    public abstract void Render(SpriteBatch batch);
    public abstract void Update(GameTime time);
  }
}
