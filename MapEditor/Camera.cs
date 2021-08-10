using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MapEditor
{
  class Camera
  {
    public int Width = 32*10*4;
    public int Height = 32*10*4;
    private Vector2 MaxPosition;
    private Vector2 MinPosition;
    public float PositionX = 32*10*2;
    public float PositionY = 32*10*2;
    public float OffsetX => PositionX - Width / 2;
    public float OffsetY => PositionY - Height / 2;
    public int scrollSpeed = 5;
    public Vector2 Offset => new Vector2(OffsetX, OffsetY);
    public Vector2 Position => new Vector2(PositionX, PositionY);
    public Vector2 Size => new Vector2(Width, Height);

    public Camera(Vector2 min, Vector2 max)
    {
      MinPosition = min;
      MaxPosition = max;
    }

    public void Move(Vector2 d)
    {
      PositionX += d.X;
      PositionY += d.Y;

      PositionX = Math.Clamp(PositionX, Width/2, MaxPosition.X - Width/2);
      PositionY = Math.Clamp(PositionY, Height / 2, MaxPosition.Y - Height / 2);
    }

    public void Update(float time)
    {
      var keyboard = Keyboard.GetState();
      if (keyboard.IsKeyDown(Keys.W))
        Move(CommonVectors.Up * scrollSpeed);
      if (keyboard.IsKeyDown(Keys.A))
        Move(CommonVectors.Left * scrollSpeed);
      if (keyboard.IsKeyDown(Keys.S))
        Move(CommonVectors.Down * scrollSpeed);
      if (keyboard.IsKeyDown(Keys.D))
        Move(CommonVectors.Right * scrollSpeed);

      if (keyboard.IsKeyDown(Keys.Add)) { Width-=10; Height-=10; }
      if (keyboard.IsKeyDown(Keys.Subtract)) { Width+=10; Height+=10; }
    }
  }
}
