using Microsoft.Xna.Framework;
using TheGame.States;
using System;
using TheGame.GameStuff;

namespace TheGame
{
  static class GameEnvironment
  {
    private static TheGame _game;
    private static StateManager stateManager;

    public static bool IsMouseVisible
    {
      get { return _game.IsMouseVisible; }
      set { _game.IsMouseVisible = value; }
    }

    public static void Init(TheGame game)
    {
      _game = game;
      stateManager = new StateManager();
    }

    public static void Exit()
    {
      _game.Exit();
    }
    public static void StartNewGame()
    {
      IsMouseVisible = false;
      stateManager.StartNewGame();
    }
    public static void SwitchToInGame()
    {
      IsMouseVisible = false;
      stateManager.SwitchToGame();
    }
    public static void SwitchToMainMenu()
    {
      IsMouseVisible = true;
      stateManager.SwitchToMainMenu();
    }
    public static State GetCurrentState() => stateManager.CurrentState;
    public static bool ExistsInGame() => stateManager.InGame != null;
    public static int ScreenWidth => _game.Graphics.PreferredBackBufferWidth;
    public static int ScreenHeight => _game.Graphics.PreferredBackBufferHeight;

    public static class Settings
    {
      public static float MusicVolume = 0.5f;
      public static float MenuVolume = 0.5f;
      public static float EffectsVolume = 0.5f;
      public static float VoiceVolume = 0.5f;
      public static int tileSize = 32;
      public static float scale = 1;
    }

  }
}
