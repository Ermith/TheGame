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
    private Camera camera;

    // public
    public int Width;
    public int Height;

    // Methods
    //=================================
    public World(Camera camera, int width, int height)
    {
      mapGenerator = new MapGenerator(width, height, 1);
      textureMapper = new TileTextureMapper();
      this.camera = camera;
      Width = width;
      Height = height;
    }

    public void CreateNew()
    {
      var option = GeneratorOption.BasicRock1;
      option.CAIterations += 4;
      tiles = mapGenerator.Generate(option);
      mapGenerator.Fill(tiles, 0, 0, GeneratorOption.LittleMoss);
      mapGenerator.LayWalls(tiles);
    }

    public void Render(SpriteBatch batch)
    {
      float scaleX = camera.ScaleX;
      float scaleY = camera.ScaleY;
      int tileSize = GameEnvironment.Settings.tileSize;
      float startX = camera.OffsetX - camera.OffsetX % tileSize;
      float startY = camera.OffsetY - camera.OffsetY % tileSize;
      Vector2 mid = new Vector2(GameEnvironment.ScreenWidth / 2, GameEnvironment.ScreenHeight / 2);
      float maxDist = mid.Length();


      for (float x = startX; x < camera.OffsetX + camera.Width; x += tileSize)
        for (float y = startY; y < camera.OffsetY + camera.Height; y += tileSize)
        {
          int tileX = (int)(x / tileSize);
          int tileY = (int)(y / tileSize);

          if (tileX >= Width || tileY >= Height)
            continue;

          camera.AbsoluteToRelative(x, y, out float xRelative, out float yRelative);

          Vector2 pos = new Vector2(xRelative * scaleX, yRelative * scaleY);
          float t = 1 - Vector2.Distance(pos, mid) / maxDist;

          
          batch.Draw(
            texture: textureMapper.Get(tiles[tileX, tileY]),
            position: pos,
            sourceRectangle: null,
            //color: Color.FromNonPremultiplied((int)(t * 255), (int)(t * 255), (int)(t * 255), 255),
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
