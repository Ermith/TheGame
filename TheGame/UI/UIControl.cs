using Microsoft.Xna.Framework;

namespace TheGame.UI
{
  abstract class UIControl
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

    public abstract void Render(RenderArguments arguments);
    public abstract void Update(UpdateArguments arguments);
  }
}
