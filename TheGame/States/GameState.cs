using TheGame.GameStuff;
using TheGame.GameStuff.ECS;
using Microsoft.Xna.Framework.Input;
using TheGame.GameStuff.ECS.Components;

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

    public override void Render(RenderArguments arguments)
    {
      GameEnvironment.IsMouseVisible = false;

      world.Render(arguments);
      entityManager.Render(arguments);
    }

    public override void Update(UpdateArguments arguments)
    {
      // Pause the game
      if (arguments.Keyboard.IsKeyDown(Keys.Escape))
      {
        CurrentState = MenuState;
        return;
      }

      // Entities
      entityManager.Update(arguments);
      var spacial = entityManager.Player.Get<CSpacial>();
      Camera.Center(spacial.Position);
    }
  }
}
