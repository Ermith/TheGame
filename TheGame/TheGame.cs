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
    private float strength = 0.5f;
    RenderTarget2D lightMask;
    RenderTarget2D mainCanvas;
    RenderTarget2D white;

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

      // Render setup
      lightMask = new RenderTarget2D(GraphicsDevice, width, height);
      mainCanvas = new RenderTarget2D(GraphicsDevice, width, height);
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      white = new RenderTarget2D(GraphicsDevice, width, height);

      DrawOnTarget(white, () => { GraphicsDevice.Clear(Color.White); });
      Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.placeHolder, 0, 0));
      GameEnvironment.Init(this);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        Exit();

      GameEnvironment.GetCurrentState().Update(gameTime);

      // Testing light effect
      if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
        strength = MathF.Max(strength - 0.05f, 0.5f);
      else
        strength = MathF.Min(strength + 0.05f, 0.85f);

      base.Update(gameTime);
    }

    private void DrawOnTarget(RenderTarget2D target, Action action)
    {
      GraphicsDevice.SetRenderTarget(target);
      GraphicsDevice.Clear(Color.Black);
      _spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
      action();
      _spriteBatch.End();
    }

    protected override void Draw(GameTime gameTime)
    {
      // Draw light
      DrawOnTarget(lightMask, () => {
        Assets.LightEffect.CurrentTechnique.Passes[0].Apply();
        var pp = Assets.LightEffect.Parameters["strength"];
        pp.SetValue(strength);
        _spriteBatch.Draw(white, Vector2.Zero, Color.White);
      });

      // Draw main stuff
      DrawOnTarget(mainCanvas, () => {
        GameEnvironment.GetCurrentState().Render(_spriteBatch);
      });

      // Mix it together
      DrawOnTarget(null, () => {
        Assets.LightingEffect.CurrentTechnique.Passes[0].Apply();
        var p = Assets.LightingEffect.Parameters["light"];
        p.SetValue(lightMask);
        _spriteBatch.Draw(mainCanvas, Vector2.Zero, Color.White);
      });

      base.Draw(gameTime);
    }
  }
}
