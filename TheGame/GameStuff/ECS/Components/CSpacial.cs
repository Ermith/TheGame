﻿using TheGame.GameStuff.ECS;
using System.Collections.Generic;
using TheGame.Math;
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Components
{
  class CSpacial : Component
  {
    public Vector2 Position { get; set; }
    public Direction Facing { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
  }
}
