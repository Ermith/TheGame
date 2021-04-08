using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TheGame.GameStuff
{
  class Camera
  {
    private static Random rnd = new Random();
    private static float intensity = 0;
    private static int zoom = 0;
    private static float offsetX;
    private static float offsetY;
    private static int defaultWidth;
    private static int defaultHeight;
    private static int MapWidth;
    private static int MapHeight;

    public static float OffsetX
    {
      get { return offsetX; }
      set { offsetX = System.Math.Clamp(value, 0, MapWidth - Width - 1); }
    }
    public static float OffsetY
    {
      get { return offsetY; }
      set { offsetY = System.Math.Clamp(value, 0, MapHeight - Height - 1); }
    }

    public static float X => OffsetX + Width / 2;
    public static float Y => OffsetY + Height / 2;

    public static int Width { get; set; }
    public static int Height { get; set; }

    public static float ScaleX => GameEnvironment.ScreenWidth/ (float)Width;
    public static float ScaleY => GameEnvironment.ScreenHeight / (float)Height;

    public static void Init(int defaultWidth, int defaultHeight, int mapWidth, int mapHeight)
    {
      Camera.defaultWidth = defaultWidth;
      Camera.defaultHeight = defaultHeight;
      MapWidth = mapWidth;
      MapHeight = mapHeight;
      ResetDimensions();
    }

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
    }

    public static void ResetDimensions()
    {
      Width = defaultWidth;
      Height = defaultHeight;
    }

    public static void Update(GameTime time)
    {
      if (intensity > 0)
        Shake();

      ResetDimensions();

      if (zoom > 0)
        Zoom();
    }

    public static void ShakeEffect(float intensity) => Camera.intensity = intensity;
    public static void ZoomEffect(int zoom) => Camera.zoom = zoom;

    private static void Shake()
    {
      float shakeX = (float)rnd.NextDouble() * intensity - intensity / 2;
      float shakeY = (float)rnd.NextDouble() * intensity - intensity / 2;

      OffsetX += shakeX;
      OffsetY += shakeY;

      intensity *= 0.9f;
      if (intensity < 0.3)
        intensity = 0;
    }
    private static void Zoom()
    {
      Width += zoom;
      Height += zoom;

      zoom = (int)(zoom * 0.3f);
    }
  }
}
