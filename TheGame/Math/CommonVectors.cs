using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Math {
  static class CommonVectors {
    public static Vector2 Left => new Vector2(-1, 0);
    public static Vector2 Right => new Vector2(1, 0);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);
  }
}
