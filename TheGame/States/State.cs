using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheGame.States
{
  abstract class State
  {
    public abstract void Render(GraphicsDevice graphics, SpriteBatch batch);
    public abstract void Update(GameTime time);
  }
}
