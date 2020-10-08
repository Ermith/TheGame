using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.GameStuff;
using TheGame.GameStuff.Entities;
using CaveGenerator;

namespace TheGame.States
{
  class GameState : State
  {
    EntityManager entityManager;
    Player Player = new Player();
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

      entityManager = new EntityManager();


      for (int i = 0; i < mapWid; i++)
      {
        for (int j = 0; j < mapHei; j++)
        {
          ColorMap[i, j] = Map[i, j] is RockTile ? Color.Black : Color.LightGray;
        }
      }

      world.CreateNew();
    }

    public override void Render(RenderArguments arguments)
    {
      Utilities.IsMouseVisible = false;

      world.Render(arguments);

      // Background
      /*/
      int xStart = Camera.OffsetX - (Camera.OffsetX % tileSize);
      int yStart = Camera.OffsetY - (Camera.OffsetY % tileSize);

      int wid = Camera.Width / tileSize + 1;
      int hei = Camera.Height / tileSize + 1;

      Utilities.ForMatrix(wid, hei, (int x, int y) =>
      {
        int xx = x * tileSize + xStart;
        int yy = y * tileSize + yStart;

        Camera.AbsoluteToRelative(xx, yy, out int ox, out int oy);

        Texture2D rect = new Texture2D(arguments.Graphics, tileSize, tileSize);
        Color[] data = new Color[tileSize * tileSize];
        Color col = ColorMap[x + Camera.OffsetX / tileSize, y + Camera.OffsetY / tileSize];

        for (int i = 0; i < tileSize * tileSize; i++) data[i] = col;
        rect.SetData(data);

        arguments.SpriteBatch.Draw(rect,
          new Vector2(ox, oy),
          Color.White
          );
      });
      /**/
      // Player

      // Entities
      entityManager.Render(arguments);
    }

    public override void Update(UpdateArguments arguments)
    {
      // Background

      // Entities
      entityManager.Update(arguments);
    }
  }
}
