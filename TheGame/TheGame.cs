using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheGame.States;

namespace TheGame
{
  public class TheGame : Game
  {
    public GraphicsDeviceManager Graphics;
    private SpriteBatch _spriteBatch;

    public TheGame()
    {
      Graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      base.Initialize();
      // TODO: Add your initialization logic here

      _spriteBatch = new SpriteBatch(GraphicsDevice);
      Graphics.PreferredBackBufferWidth = 1280;
      Graphics.PreferredBackBufferHeight = 720;
      Graphics.ApplyChanges();
      Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.placeHolder, 0, 0));

      GameEnvironment.Init(this);
    }

    protected override void LoadContent()
    {
      Assets.Load(Content);
      // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        Exit();

      // TODO: Add your update logic here
      GameEnvironment.GetCurrentState().Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      // TODO: Add your drawing code here
      _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

      //var mouse = Mouse.GetState();

      GameEnvironment.GetCurrentState().Render(_spriteBatch);

      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
