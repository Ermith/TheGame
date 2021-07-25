using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.MyMath;

namespace TheGame.GameStuff
{
  partial class Camera
  {
    private float offsetX;
    private float offsetY;
    private int defaultWidth;
    private int defaultHeight;
    private int MapWidth;
    private int MapHeight;
    public float OffsetX
    {
      get { return offsetX; }
      set { offsetX = System.Math.Clamp(value, 0, MapWidth - Width - 1); }
    }
    public float OffsetY
    {
      get { return offsetY; }
      set { offsetY = System.Math.Clamp(value, 0, MapHeight - Height - 1); }
    }
    public float X => OffsetX + Width / 2;
    public float Y => OffsetY + Height / 2;
    public int Width { get; set; }
    public int Height { get; set; }
    public float ScaleX => GameEnvironment.ScreenWidth / (float)Width;
    public float ScaleY => GameEnvironment.ScreenHeight / (float)Height;

    public Camera(int defaultWidth, int defaultHeight, int mapWidth, int mapHeight)
    {
      this.defaultWidth = defaultWidth;
      this.defaultHeight = defaultHeight;
      MapWidth = mapWidth;
      MapHeight = mapHeight;
      ResetDimensions();
    }
    public Vector2 AbsoluteToRelative(Vector2 absolute)
    {
      AbsoluteToRelative(absolute.X, absolute.Y, out float ox, out float oy);
      return new Vector2(ox, oy);
    }
    public void AbsoluteToRelative(float x, float y, out float ox, out float oy)
    {
      ox = x - OffsetX;
      oy = y - OffsetY;
    }
    public void Center(Vector2 position)
    {
      Center(position.X, position.Y);
    }
    public void Center(float x, float y)
    {
      OffsetX = x - Width / 2;
      OffsetY = y - Height / 2;
    }
    public void ResetDimensions()
    {
      Width = defaultWidth;
      Height = defaultHeight;
    }
    public void Update(GameTime time)
    {
      if (intensity > 0)
        Shake();

      ResetDimensions();
      Zoom(time);
    }
  }
}
