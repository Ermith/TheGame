using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheGame;
using CaveGenerator;
using System.Collections.Generic;
using System;

namespace MapEditor
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
      var t = type.GetType();
      return textures[type.GetType()];
    }
  }

  static class Info
  {
    public static TileType Tile = TileType.Wall;
  }


  public class MapEditor : Game
  {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private MapGenerator Generator;
    private TileType[,] Map;
    private TileTextureMapper TextureMapper;
    private MapWindow MapWindow;
    private MenuWindow MenuWindow;
    private const int mapWidth = 100;
    private const int mapHeight = 100;

    public MapEditor()
    {
      _graphics = new GraphicsDeviceManager(this);
      Generator = new MapGenerator(mapWidth, mapHeight, 1, 32);
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      Map = Generator.Generate(GeneratorOption.BasicRock1);
      Generator.Fill(Map, 0, 0, GeneratorOption.Ice);
      Generator.LayWalls(Map);

      _graphics.PreferredBackBufferWidth = 1600;
      _graphics.PreferredBackBufferHeight = 900;
      _graphics.ApplyChanges();

      
      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      Assets.Load(GraphicsDevice);
      TextureMapper = new TileTextureMapper();
      Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.WallTileTexture, 0, 0));

      MapWindow = new MapWindow(
        20, 20, 1600 / 3 * 2 - 20, 900 - 40,
        GraphicsDevice,
        Map,
        TextureMapper,
        new Camera(Vector2.Zero, new Vector2(100 * 32, 100 * 32)));

      MenuWindow = new MenuWindow(
        1600/3*2, 20, 900-40, 1600/3 - 20, GraphicsDevice, TextureMapper
        );
    }

    protected override void Update(GameTime gameTime)
    {
      MapWindow.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
      MenuWindow.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      MapWindow.Render(_spriteBatch);
      MenuWindow.Render(_spriteBatch);

      GraphicsDevice.SetRenderTarget(null);
      GraphicsDevice.Clear(Color.Black);
      _spriteBatch.Begin();
      MapWindow.RenderMain(_spriteBatch);
      MenuWindow.RenderMain(_spriteBatch);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
