using Microsoft.Xna.Framework;

namespace TheGame.GameStuff
{
  class Camera
  {
    public static int OffsetX { get; set; }
    public static int OffsetY { get; set; }

    public static Point Offset => new Point(OffsetX, OffsetY);

    public static Rectangle Rectangle => new Rectangle(Offset, Utilities.ScreenSize());
    public static int Width => Utilities.ScreenSize().X;
    public static int Height => Utilities.ScreenSize().Y;

    public static int MapWidth;
    public static int MapHeight;
    public static Point AbsoluteToRelative(Point absolute)
    {
      AbsoluteToRelative(absolute.X, absolute.Y, out int ox, out int oy);
      return new Point(ox, oy);
    }
    public static void AbsoluteToRelative(int x, int y, out int ox, out int oy)
    {
      ox = x - OffsetX;
      oy = y - OffsetY;
    }

    public static void Center(Point position)
    {
      Center(position.X, position.Y);
    }

    public static void Center(int x, int y)
    {
      OffsetX = x - Width / 2;
      OffsetY = y - Height / 2;
      OffsetX = System.Math.Clamp(OffsetX, 0, MapWidth - Width - 1);
      OffsetY = System.Math.Clamp(OffsetY, 0, MapHeight - Height - 1);
    }
  }
}
