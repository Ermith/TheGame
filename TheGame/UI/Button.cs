using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TheGame.States;

namespace TheGame.UI
{
  class Button : UIControl
  {
    // private
    private MouseState previousMouse;
    private Color color;
    private bool focused;

    // public
    public Func<bool> IsActive = () => true;
    public SpriteFont Font { get; set; }
    public string Text { get; set; }
    public Rectangle Rectangle => new Rectangle(Location.ToPoint(), Font.MeasureString(Text).ToPoint());
    public event Action Click;

    // Methods
    //=================================
    public Button(Vector2 location, string text, SpriteFont font) : base(location)
    {
      Text = text;
      Font = font;
    }
    public Button(float x, float y, string text, SpriteFont font) : base(x, y)
    {
      Text = text;
      Font = font;
    }
    public override void Render(SpriteBatch batch)
    {
      batch.DrawString(Font, Text, Location, color);
    }
    public override void Update(GameTime time)
    {
      color = Color.White;
      if (!IsActive()) return;

      MouseState mouse = Mouse.GetState();

      if (Rectangle.Contains(mouse.Position))
      { // is Hovering
        color = Color.Gray;
        if (!focused) Assets.Click.Play(GameEnvironment.Settings.MenuVolume, 0.0f, 0.0f);
        focused = true;

        if (previousMouse.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released)
        {
          Click?.Invoke();
        }
      }
      else
      {
        focused = false;
      }

      previousMouse = mouse;
    }

    // Frequently used buttons
    //=================================
    public static Button Exit()
    {
      Button exit = new Button(new Vector2(), "Exit", Assets.testFont);
      exit.Click += GameEnvironment.Exit;

      return exit;
    }
    public static Button NewGame()
    {
      Button newGame = new Button(new Vector2(), "New Game", Assets.testFont);
      newGame.Click += NewGame_Click; 

      return newGame;
    }
    private static void NewGame_Click()
    {
      GameEnvironment.StartNewGame();
    }
    public static Button ResumeGame()
    {
      Button resumeGame = new Button(new Vector2(), "Resume Game", Assets.testFont);
      resumeGame.Click += ResumeGame_Click;
      resumeGame.IsActive = () => { return GameEnvironment.ExistsInGame(); };

      return resumeGame;
    }
    private static void ResumeGame_Click()
    {
      GameEnvironment.SwitchToInGame();
    }
  }
}
