using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TheGame.UI
{
  public class Button : UIControl
  {
    // private
    private MouseState previousMouse;
    private Color color;
    private bool focused;

    // public
    public Func<bool> IsActive = () => true;
    public Texture2D Image { get; set; }
    public SpriteFont Font { get; set; }
    public string Text { get; set; }
    public SoundEffect ClickSound { get; set; }
    public Rectangle Rectangle => (Font != null) ? new Rectangle(Location.ToPoint(), Font.MeasureString(Text).ToPoint()) : new Rectangle(Location.ToPoint(), Image.Bounds.Size);
    public event Action Click;

    // Methods
    //=================================
    private void init(string text, SpriteFont font, Texture2D image, SoundEffect clickSound, Action click)
    {
      Text = text;
      Font = font;
      Image = image;
      ClickSound = clickSound;
      if (click != null)
        Click += click;
      
    }

    public Button(
      Vector2 location,
      string text = null,
      SpriteFont font = null,
      Texture2D image = null,
      SoundEffect clickSound = null,
      Action click = null,
      Vector2 offset = new Vector2()) 
      : base(location, offset)
    {
      init(text, font, image, clickSound, click);
    }
    public Button(float x, float y,
      string text = null,
      SpriteFont font = null,
      Texture2D image = null,
      SoundEffect clickSound = null,
      Action click = null,
      Vector2 offset = new Vector2())
      : base(x, y, offset)
    {
      init(text, font, image, clickSound, click);
    }
    public override void Render(SpriteBatch batch)
    {
      if (Image != null)
        batch.Draw(Image, Rectangle, color);

      if (Text != null && Font != null)
        batch.DrawString(Font, Text, Location, color);
    }
    public override void Update(float time)
    {
      color = Color.White;
      if (!IsActive()) return;

      MouseState mouse = Mouse.GetState();

      if (Rectangle.Contains(mouse.Position - Offset.ToPoint()))
      { // is Hovering
        color = Color.Gray;
        if (!focused && ClickSound != null) ClickSound.Play(GameEnvironment.Settings.MenuVolume, 0.0f, 0.0f);
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
      Button exit = new Button(new Vector2(), "Exit", Assets.testFont, clickSound: Assets.Click);
      exit.Click += GameEnvironment.Exit;

      return exit;
    }
    public static Button NewGame()
    {
      Button newGame = new Button(new Vector2(), "New Game", Assets.testFont, clickSound: Assets.Click);
      newGame.Click += NewGame_Click; 

      return newGame;
    }
    private static void NewGame_Click()
    {
      GameEnvironment.StartNewGame();
    }
    public static Button ResumeGame()
    {
      Button resumeGame = new Button(new Vector2(), "Resume Game", Assets.testFont, clickSound: Assets.Click);
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
