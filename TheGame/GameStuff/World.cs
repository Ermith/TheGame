using CaveGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TheGame.GameStuff
{

  class TileTextureMapper
  {
    private readonly Dictionary<Type, Texture2D> textures = new Dictionary<Type, Texture2D>();

    public TileTextureMapper()
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
    private readonly MapGenerator mapGenerator;
    private readonly TileTextureMapper textureMapper;
    private TileType[,] tiles;

    // public
    public int Width = 100;
    public int Height = 100;

    // Methods
    //=================================
    public World()
    {
      mapGenerator = new MapGenerator(Width, Height, 1);
      textureMapper = new TileTextureMapper();
    }

    public void CreateNew()
    {
      tiles = mapGenerator.Generate(GeneratorOption.BasicRock1);
      mapGenerator.Fill(tiles, 0, 0, GeneratorOption.Ice);
      mapGenerator.LayWalls(tiles);
    }

    public void Render(SpriteBatch batch)
    {
      float scaleX = Camera.ScaleX;
      float scaleY = Camera.ScaleY;
      int tileSize = GameEnvironment.Settings.tileSize;
      float startX = Camera.OffsetX - Camera.OffsetX % tileSize;
      float startY = Camera.OffsetY - Camera.OffsetY % tileSize;

      for (float x = startX; x < Camera.OffsetX + Camera.Width; x += tileSize)
        for (float y = startY; y < Camera.OffsetY + Camera.Height; y += tileSize)
        {
          int tileX = (int)(x / tileSize);
          int tileY = (int)(y / tileSize);

          if (tileX >= Width || tileY >= Height)
            continue;

          Camera.AbsoluteToRelative(x, y, out float xRelative, out float yRelative);

          batch.Draw(
            texture: textureMapper.Get(tiles[tileX, tileY]),
            position: new Vector2(xRelative * scaleX, yRelative * scaleY),
            sourceRectangle: null,
            color: Color.White,
            rotation: 0,
            origin: Vector2.Zero,
            scale: new Vector2(scaleX, scaleY),
            effects: SpriteEffects.None,
            layerDepth: 0
            );
        }
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
