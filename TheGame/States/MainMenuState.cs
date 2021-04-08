using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.UI;

namespace TheGame.States
{
  class MainMenuState : State
  {
    private readonly UIControlManager manager;

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

      Point screenSize = GameEnvironment.ScreenSize();
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

    public override void Render(SpriteBatch batch) => manager.Render(batch);
    public override void Update(GameTime time) => manager.Update(time);

  }
}
