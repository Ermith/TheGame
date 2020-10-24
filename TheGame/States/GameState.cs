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
      // constructors
      world = new World();
      entityManager = new EntityManager(world);

      // world and camera
      world.CreateNew();
      Camera.MapWidth = world.Width * Utilities.Settings.tileSize;
      Camera.MapHeight = world.Height * Utilities.Settings.tileSize;

      // entities
      CSpacial spacial = entityManager.Player.Components[ComponentTypes.Spacial] as CSpacial;
      spacial.Position = world.GenerateSpawnPoint();
    }

    public override void Render(RenderArguments arguments)
    {
      Utilities.IsMouseVisible = false;

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
      var spacial = entityManager.Player.Components[ComponentTypes.Spacial] as CSpacial;
      Camera.Center(spacial.Position);
    }
  }
}
