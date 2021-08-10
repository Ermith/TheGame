using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.UI
{
  public abstract class UIControl
  {
    public Vector2 Location;
    public Vector2 Offset;

    public UIControl(Vector2 location, Vector2 offset)
    {
      Location = location;
      Offset = offset;
    }

    public UIControl(float x, float y, Vector2 offset)
    {
      Location = new Vector2(x, y);
      Offset = offset;
    }

    public abstract void Render(SpriteBatch batch);
    public abstract void Update(float time);
  }
}
