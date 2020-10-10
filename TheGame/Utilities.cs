using Microsoft.Xna.Framework;
using System;

namespace TheGame
{
  static class Utilities
  {
    private static TheGame _game;

    public static bool IsMouseVisible
    {
      get { return _game.IsMouseVisible; }
      set { _game.IsMouseVisible = value; }
    }

    public static void Init(TheGame game)
    {
      _game = game;
    }

    public static void Exit()
    {
      _game.Exit();
    }

    public static Point ScreenSize()
    {
      return new Point(
        _game.Graphics.PreferredBackBufferWidth,
        _game.Graphics.PreferredBackBufferHeight
        );
    }

    public static class Settings
    {
      public static float MusicVolume = 0.5f;
      public static float MenuVolume = 0.5f;
      public static float EffectsVolume = 0.5f;
      public static float VoiceVolume = 0.5f;
      public static int tileSize = 32;
    }

    public static void ForMatrix(int width, int height, Action<int, int> action)
    {
      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          action(i, j);
        }
      }
    }
  }
}
