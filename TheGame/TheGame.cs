using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheGame.States;

namespace TheGame {
  public class TheGame : Game {
    public GraphicsDeviceManager Graphics;
    private SpriteBatch _spriteBatch;
    Texture2D iceTile;

    public TheGame() {
      Graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize() {
      // TODO: Add your initialization logic here
      Assets.Load(Content);
      Utilities.Init(this);
      Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.placeHolder, 0, 0));

      State.CurrentState = new MainMenuState();

      base.Initialize();
    }

    protected override void LoadContent() {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
      iceTile = Assets.placeHolder;
    }

    protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here
      State.CurrentState.Update(new UpdateArguments {
        Game = this,
        Time = gameTime,
        Mouse = Mouse.GetState(),
        Keyboard = Keyboard.GetState()
      });

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      // TODO: Add your drawing code here
      _spriteBatch.Begin();

      //var mouse = Mouse.GetState();

      State.CurrentState.Render(new RenderArguments {
        Time = gameTime,
        Graphics = GraphicsDevice,
        GraphicsManager = Graphics,
        SpriteBatch = _spriteBatch
      });

      //_spriteBatch.Draw(iceTile, new Rectangle(mouse.Position, new Point(iceTile.Width, iceTile.Height)), Color.White);

      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
