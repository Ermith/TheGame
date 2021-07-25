using TheGame.GameStuff;
using TheGame.GameStuff.ECS;
using Microsoft.Xna.Framework.Input;
using TheGame.GameStuff.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheGame.States
{
  class GameState : State
  {
    private SystemManager systemManager;
    private World world;
    private Camera camera;

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

    public override void Render(SpriteBatch batch)
    {
      world.Render(batch);
      systemManager.Render(batch);
    }

    public override void Update(GameTime time)
    {
      // Pause the game
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
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
