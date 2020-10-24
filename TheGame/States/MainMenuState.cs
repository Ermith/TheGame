﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TheGame.UI;

namespace TheGame.States
{
  class MainMenuState : State
  {
    private UIControlManager manager;

    public MainMenuState()
    {
      manager = new UIControlManager();

      var buttons = new List<Button> {
        Button.NewGame(),
        Button.ResumeGame(),
        Button.Exit()
      };

      int buttonsHeight = 0;

      foreach (Button button in buttons)
      {
        buttonsHeight += button.Rectangle.Height;
      }

      Point screenSize = Utilities.ScreenSize();
      int freeSpace = screenSize.Y - buttonsHeight;

      // divide into sufficient parts
      freeSpace /= buttons.Count + 1;
      int y = 0;

      foreach (Button button in buttons)
      {
        y += freeSpace;
        int x = screenSize.X / 2 - button.Rectangle.Width / 2;

        button.Location = new Vector2(x, y);
        manager.AddControl(button);

        y += button.Rectangle.Height;
      }
    }

    public override void Render(RenderArguments arguments)
    {
      Utilities.IsMouseVisible = true;
      manager.Render(arguments);
    }
    public override void Update(UpdateArguments arguments) => manager.Update(arguments);

  }
}
