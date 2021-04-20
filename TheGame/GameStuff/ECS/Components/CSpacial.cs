using TheGame.GameStuff.ECS;
using System.Collections.Generic;
using TheGame.Math;
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Components
{
  class CSpacial : Component
  {
    public float X;
    public float Y;
    public Vector2 Position {
      get => new Vector2(X, Y);
      set {
        X = value.X;
        Y = value.Y;
      }
    }
    public int Width { get; set; }
    public int Height { get; set; }
    public Rectangle HitBox => new Rectangle((int)X - Width / 2, (int)Y - Height / 2, Width, Height);
  }
}
