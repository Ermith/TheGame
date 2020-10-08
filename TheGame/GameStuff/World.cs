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
    private MapGenerator mapGenerator;
    private TileFactory factory;
    TileType[,] tiles;
    int Width = 100;
    int Height = 100;

    public World()
    {
      mapGenerator = new MapGenerator(Width, Height, 1);
      factory = new TileFactory();
    }

    public void CreateNew()
    {
      tiles = mapGenerator.Generate(GeneratorOption.BasicRock1);
    }

    public void Render(RenderArguments arguments)
    {
      int xStart = Camera.OffsetX - (Camera.OffsetX % Utilities.Settings.tileSize);
      int yStart = Camera.OffsetY - (Camera.OffsetY % Utilities.Settings.tileSize);

      Utilities.ForMatrix(
        Camera.Width / Utilities.Settings.tileSize + 1,
        Camera.Height / Utilities.Settings.tileSize + 1,
        (int x, int y) =>
        {
          int xx = x * Utilities.Settings.tileSize + xStart;
          int yy = y * Utilities.Settings.tileSize + yStart;

          Camera.AbsoluteToRelative(xx, yy, out int ox, out int oy);

          arguments.SpriteBatch.Draw(
            factory.Get(
              tiles[
              x + Camera.OffsetX / Utilities.Settings.tileSize,
              y + Camera.OffsetY / Utilities.Settings.tileSize
              ]),
            new Vector2(ox, oy),
            Color.White
            );
        });
    }
  }
}
