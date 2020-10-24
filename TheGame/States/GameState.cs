using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.GameStuff;
using TheGame.GameStuff.Entities;
using CaveGenerator;
using Microsoft.Xna.Framework.Input;
using TheGame.GameStuff.Components;

namespace TheGame.States
{
  class GameState : State
  {
    EntityManager entityManager;
    World world = new World();

    // Prototype
    private TileType[,] Map;
    private Color[,] ColorMap;
    private MapGenerator mapgen;
    private int mapWid = 100;
    private int mapHei = 100;

    public GameState()
    {
      mapgen = new MapGenerator(mapWid, mapHei, 1);
      Map = mapgen.Generate(GeneratorOption.BasicRock1);
      ColorMap = new Color[mapWid, mapHei];

      Camera.MapWidth = mapWid * Utilities.Settings.tileSize;
      Camera.MapHeight = mapHei * Utilities.Settings.tileSize;

      entityManager = new EntityManager(world);


      for (int i = 0; i < mapWid; i++)
      {
        for (int j = 0; j < mapHei; j++)
        {
          ColorMap[i, j] = Map[i, j] is RockTile ? Color.Black : Color.LightGray;
        }
      }

      world.CreateNew();
      var spawnPoint = world.GenerateSpawnPoint();
      CLocation location = entityManager.Player.Components[Component.Components.Location] as CLocation;
      location.X = (int)spawnPoint.X;
      location.Y = (int)spawnPoint.Y;
    }

    public override void Render(RenderArguments arguments)
    {
      Utilities.IsMouseVisible = false;

      world.Render(arguments);

      // Entities
      entityManager.Render(arguments);
    }

    public override void Update(UpdateArguments arguments)
    {
      if (arguments.Keyboard.IsKeyDown(Keys.Escape))
      {
        State.CurrentState = State.MenuState;
        return;
      }
      // Entities
      entityManager.Update(arguments);
      var l = entityManager.Player.Components[Component.Components.Location] as CLocation;
      Camera.Center((int)l.X, (int)l.Y);
    }
  }
}
