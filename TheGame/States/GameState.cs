using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.GameStuff;
using TheGame.GameStuff.ECS;
using CaveGenerator;
using Microsoft.Xna.Framework.Input;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.States
{
  class GameState : State
  {
    EntityManager entityManager;
    World world;

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
      var spawnPoint = world.GenerateSpawnPoint();
      CLocation location = entityManager.Player.Components[ComponentTypes.Location] as CLocation;
      location.X = (int)spawnPoint.X;
      location.Y = (int)spawnPoint.Y;
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
      var l = entityManager.Player.Components[ComponentTypes.Location] as CLocation;
      Camera.Center((int)l.X, (int)l.Y);
    }
  }
}
