using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

      int screenWid = GameEnvironment.ScreenWidth;
      int screeenHei = GameEnvironment.ScreenHeight;
      int freeSpace = screeenHei - buttonsHeight;

      // divide into sufficient parts
      freeSpace /= buttons.Count + 1;
      int y = 0;

      foreach (Button button in buttons)
      {
        y += freeSpace;
        int x = screenWid / 2 - button.Rectangle.Width / 2;

        button.Location = new Vector2(x, y);
        manager.AddControl(button);

        y += button.Rectangle.Height;
      }
    }

    public override void Render(GraphicsDevice graphics, SpriteBatch batch)
    {
      graphics.Clear(Color.Black);
      batch.Begin();
      manager.Render(batch);
      batch.End();
    }
    public override void Update(GameTime time)
    {
      manager.Update((float)time.ElapsedGameTime.TotalMilliseconds);
    }
  }
}
