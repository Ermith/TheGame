using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.Math
{
  static class CommonVectors
  {
    public static Vector2 Left => new Vector2(-1, 0);
    public static Vector2 Right => new Vector2(1, 0);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);

    public static Vector2 GetDirection(Direction dir)
    {
      switch (dir)
      {
        case Direction.Up: return Up;
        case Direction.Left: return Left;
        case Direction.Down: return Down;
        case Direction.Right: return Right;
        default: return Vector2.Zero;
      }
    }
  }
}