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

    public GameState()
    {
      // world and camera
      world = new World();
      world.CreateNew();
      Camera.Init(
        16*25, 9*25,
        world.Width * GameEnvironment.Settings.tileSize,
        world.Height * GameEnvironment.Settings.tileSize);

      // entities
      systemManager = new SystemManager(world);
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
      Camera.Center(spacial.Position);
      Camera.Update(time);
    }
  }
}
