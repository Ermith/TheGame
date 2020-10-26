using CaveGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TheGame.GameStuff
{

  class TileFactory
  {
    private readonly Dictionary<Type, Texture2D> textures = new Dictionary<Type, Texture2D>();

    public TileFactory()
    {
      Add<FloorTile>(Assets.FloorTileTexture);
      Add<RockTile>(Assets.RockTileTexture);
      Add<WallTile>(Assets.WallTileTexture);
      Add<MossTile>(Assets.MossTileTexture);
      Add<MossFloorTile>(Assets.MossFloorTileTexture);
      Add<MushroomTile>(Assets.MushroomTileTexture);
      Add<MushroomFloorTile>(Assets.MushroomFloorTileTexture);
      Add<IceTile>(Assets.IceTileTexture);
      Add<IceFloorTile>(Assets.IceFloorTileTexture);
      Add<MagmaTile>(Assets.MagmaTileTexture);
      Add<MagmaFloorTile>(Assets.MagmaFloorTileTexture);
      Add<FleshTile>(Assets.FleshTileTexture);
      Add<FleshFloorTile>(Assets.FleshFloorTileTexture);
    }


    public void Add<T>(Texture2D texture)
    {
      textures.Add(typeof(T), texture);
    }

    public void Add(TileType type, Texture2D texture)
    {
      textures.Add(type.GetType(), texture);
    }

    public Texture2D Get(TileType type)
    {
      return textures[type.GetType()];
    }
  }

  class World : IRenderable
  {
    // private
    private MapGenerator mapGenerator;
    private TileFactory factory;
    private TileType[,] tiles;

    // public
    public int Width = 100;
    public int Height = 100;

    // Methods
    //=================================
    public World()
    {
      mapGenerator = new MapGenerator(Width, Height, 1);
      factory = new TileFactory();
    }

    public void CreateNew()
    {
      tiles = mapGenerator.Generate(GeneratorOption.BasicRock1);
      mapGenerator.Fill(tiles, 0, 0, GeneratorOption.Ice);
      mapGenerator.LayWalls(tiles);
    }

    public void Render(RenderArguments arguments)
    {
      float xStart = Camera.OffsetX - (Camera.OffsetX % GameEnvironment.Settings.tileSize);
      float yStart = Camera.OffsetY - (Camera.OffsetY % GameEnvironment.Settings.tileSize);

      GameEnvironment.ForMatrix(
        Camera.Width / GameEnvironment.Settings.tileSize + 1,
        Camera.Height / GameEnvironment.Settings.tileSize + 1,
        (int x, int y) =>
        {
          float xx = x * GameEnvironment.Settings.tileSize + xStart;
          float yy = y * GameEnvironment.Settings.tileSize + yStart;

          Camera.AbsoluteToRelative(xx, yy, out float ox, out float oy); ;

          arguments.SpriteBatch.Draw(
            factory.Get(
              tiles[
              (int)(x + Camera.OffsetX / GameEnvironment.Settings.tileSize),
              (int)(y + Camera.OffsetY / GameEnvironment.Settings.tileSize)
              ]),
            new Vector2(ox, oy),
            Color.White
            );
        });
    }

    public Vector2 GenerateSpawnPoint()
    {
      mapGenerator.GenerateSpawn(out int x, out int y);
      return new Vector2(x * GameEnvironment.Settings.tileSize, y* GameEnvironment.Settings.tileSize);
    }

    public bool CheckPosition(Rectangle rect)
    {
      int tileSize = GameEnvironment.Settings.tileSize;

      int xStart = rect.X / tileSize;
      int yStart = rect.Y / tileSize;

      int xEnd = (rect.X + rect.Width) / tileSize;
      int yEnd = (rect.Y + rect.Height) / tileSize;

      for (int x = xStart; x <= xEnd; x++)
        for (int y = yStart; y <= yEnd; y++)
          if (tiles[x, y].Solid)
            return false;

      return true;
    }
  }
}
