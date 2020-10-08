using Microsoft.Xna.Framework;

namespace TheGame.GameStuff
{
  class Camera
  {
    public static Point Offset;
    public static Rectangle Rectangle => new Rectangle(Offset, Utilities.ScreenSize());
    public static int Width => Utilities.ScreenSize().X;
    public static int Height => Utilities.ScreenSize().Y;
    public static Point AbsoluteToRelative(Point absolute)
    {
      return absolute - Offset;
    }
    public static void AbsoluteToRelative(int x, int y, out int ox, out int oy)
    {
      ox = x - Offset.X;
      oy = y - Offset.Y;
    }
    public static void Center(Point position)
    {
      Center(position.X, position.Y);
    }

    public static void Center(int x, int y)
    {
      Offset = new Point(x - Width / 2, y - Height / 2);
      Offset.X = System.Math.Clamp(Offset.X, 0, Width - 1);
      Offset.Y = System.Math.Clamp(Offset.Y, 0, Height - 1);
    }
  }
}
