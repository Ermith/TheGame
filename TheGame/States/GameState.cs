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
    private EntityManager entityManager;
    private World world;

    public GameState()
    {
      // world and camera
      world = new World();
      world.CreateNew();
      Camera.MapWidth = world.Width * GameEnvironment.Settings.tileSize;
      Camera.MapHeight = world.Height * GameEnvironment.Settings.tileSize;

      // entities
      entityManager = new EntityManager(world);
    }

    public override void Render(SpriteBatch batch)
    {
      world.Render(batch);
      entityManager.Render(batch);
    }

    public override void Update(GameTime time)
    {
      // Pause the game
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        GameEnvironment.SwitchToMainMenu();
        return;
      }

      // Entities
      entityManager.Update(time);
      var spacial = entityManager.Player.Get<CSpacial>();
      Camera.Center(spacial.Position);
    }
  }
}
