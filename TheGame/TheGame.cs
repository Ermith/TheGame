using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
      //Graphics.IsFullScreen = true;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    // this happens first
    protected override void LoadContent()
    {
      Assets.Load(Content);
    }

    // this happens second
    protected override void Initialize()
    {
      base.Initialize();
      // Resolution
      int width = 16 * 100;
      int height = 9 * 100;
      Graphics.PreferredBackBufferWidth = width;
      Graphics.PreferredBackBufferHeight = height;
      Graphics.ApplyChanges();

      Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.placeHolder, 0, 0));
      GameEnvironment.Init(this);
      _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        Exit();

      GameEnvironment.GetCurrentState().Update(gameTime);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GameEnvironment.GetCurrentState().Render(GraphicsDevice, _spriteBatch);
      base.Draw(gameTime);
    }
  }
}
