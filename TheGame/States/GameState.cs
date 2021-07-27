using TheGame.GameStuff;
using TheGame.GameStuff.ECS;
using Microsoft.Xna.Framework.Input;
using TheGame.GameStuff.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TheGame.States
{
  class GameState : State
  {
    private SystemManager systemManager;
    private World world;
    private Camera camera;
    private RenderTarget2D lightMask;
    private RenderTarget2D mainCanvas;


    public GameState()
    {
      // world and camera
      int width = 100;
      int height = 100;

      camera = new Camera(16 * 25, 9 * 25,
        width * GameEnvironment.Settings.tileSize,
        height * GameEnvironment.Settings.tileSize);
      world = new World(camera, width, height);
      world.CreateNew();

      // entities
      systemManager = new SystemManager(world, camera);
    }


    public override void Render(GraphicsDevice graphics, SpriteBatch batch)
    {
      // Render setup
      if (lightMask == null)
        lightMask = new RenderTarget2D(graphics, GameEnvironment.ScreenWidth, GameEnvironment.ScreenHeight);

      if (mainCanvas == null)
        mainCanvas = new RenderTarget2D(graphics, GameEnvironment.ScreenWidth, GameEnvironment.ScreenHeight);

      // Draw all the main stuff
      graphics.SetRenderTarget(mainCanvas);
      graphics.Clear(Color.Black);
      batch.Begin(samplerState: SamplerState.PointClamp);
      world.Render(batch);
      systemManager.Render(batch);
      batch.End();

      // Draw light
      /**/
      graphics.SetRenderTarget(lightMask);
      graphics.Clear(Color.Black);
      batch.Begin(blendState: BlendState.Additive);
      systemManager.RenderLight(batch);
      batch.End();
      /**/

      // Mix it together
      /**/
      graphics.SetRenderTarget(null);
      graphics.Clear(Color.Black);
      batch.Begin(sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend);
      Assets.LightingEffect.CurrentTechnique.Passes[0].Apply();
      var p = Assets.LightingEffect.Parameters["light"];
      p.SetValue(lightMask);
      batch.Draw(mainCanvas, Vector2.Zero, Color.White);
      batch.End();
      /**/
    }

    public override void Update(GameTime time)
    {
      // Pause the game
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        GameEnvironment.SwitchToMainMenu();
        return;
      }

      if (systemManager.Player.Get<CHealth>().HealthPoints <= 0)
      {
        GameEnvironment.SwitchToMainMenu();
        return;
      }

      systemManager.Update(time);

      // Center the camera on player
      var spacial = systemManager.Player.Get<CSpacial>();
      camera.Center(spacial.Position);
      camera.Update(time);
    }
  }
}
