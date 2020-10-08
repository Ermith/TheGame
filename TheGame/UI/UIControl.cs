using Microsoft.Xna.Framework;

namespace TheGame.UI
{
  abstract class UIControl
  {
    public Point Location;

    public UIControl(Point location)
    {
      Location = location;
    }

    public UIControl(int x, int y)
    {
      Location = new Point(x, y);
    }

    public abstract void Render(RenderArguments arguments);
    public abstract void Update(UpdateArguments arguments);
  }
}
