using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.UI
{
  abstract class UIControl : IGameComponent
  {
    public Vector2 Location;

    public UIControl(Vector2 location)
    {
      Location = location;
    }

    public UIControl(float x, float y)
    {
      Location = new Vector2(x, y);
    }

    public abstract void Render(SpriteBatch batch);
    public abstract void Update(GameTime time);
  }
}
