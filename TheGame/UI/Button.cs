using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TheGame.States;

namespace TheGame.UI
{


  class Button : UIControl
  {
    private MouseState previousMouse;
    private Color color;
    private bool focused;


    public Func<bool> IsActive = () => true;
    public SpriteFont Font { get; set; }
    public string Text { get; set; }
    public Rectangle Rectangle => new Rectangle(Location, Font.MeasureString(Text).ToPoint());
    public event Action Click;

    // Methods
    public Button(Point location, string text, SpriteFont font) : base(location)
    {
      Text = text;
      Font = font;
    }

    public Button(int x, int y, string text, SpriteFont font) : base(x, y)
    {
      Text = text;
      Font = font;
    }

    public override void Render(RenderArguments arguments)
    {
      arguments.SpriteBatch.DrawString(Font, Text, Location.ToVector2(), color);
    }

    public override void Update(UpdateArguments arguments)
    {
      color = Color.White;
      if (!IsActive()) return;

      if (Rectangle.Contains(arguments.Mouse.Position))
      { // is Hovering
        color = Color.Gray;
        if (!focused) Assets.Click.Play(Utilities.Settings.MenuVolume, 0.0f, 0.0f);
        focused = true;

        if (previousMouse.LeftButton == ButtonState.Pressed && arguments.Mouse.LeftButton == ButtonState.Released)
        {
          Click?.Invoke();
        }
      }
      else
      {
        focused = false;
      }

      previousMouse = arguments.Mouse;

    }

    // Frequently used buttons
    public static Button Exit()
    {
      Button exit = new Button(new Point(), "Exit", Assets.testFont);
      exit.Click += Utilities.Exit;

      return exit;
    }

    public static Button NewGame()
    {
      Button newGame = new Button(new Point(), "New Game", Assets.testFont);
      newGame.Click += NewGame_Click; 

      return newGame;
    }

    private static void NewGame_Click()
    {
      State.GameState = new GameState();
      State.CurrentState = State.GameState;
    }

    public static Button ResumeGame()
    {
      Button resumeGame = new Button(new Point(), "Resume Game", Assets.testFont);
      resumeGame.Click += ResumeGame_Click;
      resumeGame.IsActive = () => { return State.GameState != null; };

      return resumeGame;
    }

    private static void ResumeGame_Click()
    {
      State.CurrentState = State.GameState;
    }
  }
}
