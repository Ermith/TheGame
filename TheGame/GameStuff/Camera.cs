using Microsoft.Xna.Framework;

namespace TheGame.GameStuff
{
  class Camera
  {
    public static float OffsetX { get; set; }
    public static float OffsetY { get; set; }

    public static float X => OffsetX + Width / 2;
    public static float Y => OffsetY + Height / 2;

    public static float ScaleX => GameEnvironment.ScreenSize().X / Width;
    public static float ScaleY => GameEnvironment.ScreenSize().Y / Height;

    public static Vector2 Offset => new Vector2(OffsetX, OffsetY);

    public static Rectangle Rectangle => new Rectangle((int)Offset.X, (int)Offset.Y, Width, Height);
    public static int Width { get; set; } = 800;
    public static int Height { get; set; } = 480;

    public static int MapWidth;
    public static int MapHeight;
    public static Vector2 AbsoluteToRelative(Vector2 absolute)
    {
      AbsoluteToRelative(absolute.X, absolute.Y, out float ox, out float oy);
      return new Vector2(ox, oy);
    }
    public static void AbsoluteToRelative(float x, float y, out float ox, out float oy)
    {
      ox = x - OffsetX;
      oy = y - OffsetY;
    }
    public static void Center(Vector2 position)
    {
      Center(position.X, position.Y);
    }
    public static void Center(float x, float y)
    {
      OffsetX = x - Width / 2;
      OffsetY = y - Height / 2;
      OffsetX = System.Math.Clamp(OffsetX, 0, MapWidth - Width - 1);
      OffsetY = System.Math.Clamp(OffsetY, 0, MapHeight - Height - 1);
    }
  }
}
