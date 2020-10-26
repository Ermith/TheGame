using System;
using System.Collections.Generic;
using System.Text;
using TheGame.States;

namespace TheGame
{
  class StateManager
  {
    public State CurrentState { get; private set; }
    public MainMenuState MainMenu { get; private set; }
    public GameState InGame { get; private set; }
    public StateManager()
    {
      MainMenu = new MainMenuState();
      CurrentState = MainMenu;
    }

    public void StartNewGame()
    {
      InGame = new GameState();
      SwitchToGame();
    }

    public void SwitchToGame() =>  CurrentState = InGame;
    public void SwitchToMainMenu() => CurrentState = MainMenu;
  }
}
