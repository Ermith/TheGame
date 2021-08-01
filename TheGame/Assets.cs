using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TheGame.GameStuff.ECS;
using TheGame.GameStuff.ECS.Components;

namespace TheGame
{
  static class Assets
  {
    public static SpriteFont testFont;

    // Sounds
    public static SoundEffect Click;
    public static SoundEffect Cut;
    public static SoundEffect Swoosh;

    // tiles
    public static Texture2D FloorTileTexture;
    public static Texture2D RockTileTexture;
    public static Texture2D WallTileTexture;
    public static Texture2D MossTileTexture;
    public static Texture2D MossFloorTileTexture;
    public static Texture2D MushroomTileTexture;
    public static Texture2D MushroomFloorTileTexture;
    public static Texture2D IceTileTexture;
    public static Texture2D IceFloorTileTexture;
    public static Texture2D MagmaTileTexture;
    public static Texture2D MagmaFloorTileTexture;
    public static Texture2D FleshTileTexture;
    public static Texture2D FleshFloorTileTexture;

    // Other textures
    public static Texture2D placeHolder;
    public static Texture2D LightMask;
    public static Texture2D Rectangle;
    public static Texture2D Cursor;

    // Characters
    public static Texture2D KnightSprite;
    public static Texture2D RogueSprite;
    public static Texture2D EnemySprite;

    // Overlays
    public static Texture2D[] FireOverlay;
    public static Texture2D[] ShadowOverlay;

    // Animations
    public static CAnimation RogueAnimataion;
    public static CAnimation KnightAnimation;
    public static CAnimation EnemyAnimation;

    // Shaders
    public static Effect LightEffect;
    public static Effect LightingEffect;

    static public void Load(ContentManager content)
    {
      // Tiles
      FloorTileTexture = content.Load<Texture2D>("Textures/Tiles/FloorTile");
      RockTileTexture = content.Load<Texture2D>("Textures/Tiles/RockTile");
      WallTileTexture = content.Load<Texture2D>("Textures/Tiles/WallTile");
      MossTileTexture = content.Load<Texture2D>("Textures/Tiles/MossTile");
      MossFloorTileTexture = content.Load<Texture2D>("Textures/Tiles/MossFloorTile");
      MushroomTileTexture = content.Load<Texture2D>("Textures/Tiles/MushroomTile");
      MushroomFloorTileTexture = content.Load<Texture2D>("Textures/Tiles/MushroomFloorTile");
      IceTileTexture = content.Load<Texture2D>("Textures/Tiles/IceTile");
      IceFloorTileTexture = content.Load<Texture2D>("Textures/Tiles/IceFloorTile");
      MagmaTileTexture = content.Load<Texture2D>("Textures/Tiles/MagmaTile");
      MagmaFloorTileTexture = content.Load<Texture2D>("Textures/Tiles/MagmaFloorTile");
      FleshTileTexture = content.Load<Texture2D>("Textures/Tiles/FleshTile");
      FleshFloorTileTexture = content.Load<Texture2D>("Textures/Tiles/FleshFloorTile");

      // Character sprites and animations
      KnightSprite = content.Load<Texture2D>("Textures/Characters/KnightSpriteSheet");
      EnemySprite = content.Load<Texture2D>("Textures/Characters/EnemySpriteSheet");
      RogueSprite = content.Load<Texture2D>("Textures/Characters/RogueSpriteSheet");

      KnightAnimation = AnimationParser.ParseSettings("SpriteInfo/KnightInfo.txt", KnightSprite);
      RogueAnimataion = AnimationParser.ParseSettings("SpriteInfo/RogueInfo.txt", RogueSprite);
      EnemyAnimation = AnimationParser.ParseSettings("SpriteInfo/EnemyInfo.txt", EnemySprite);

      // Overlays
      FireOverlay = new Texture2D[46];
      for (int i = 0; i < 46; i++)
        FireOverlay[i] = content.Load<Texture2D>($"Textures/Fire/fire_{i}");

      ShadowOverlay = new Texture2D[46];
      for (int i = 0; i < 46; i++)
        ShadowOverlay[i] = content.Load<Texture2D>($"Textures/Shadow/shadow_{i}");

      // Other textures
      placeHolder = content.Load<Texture2D>("Textures/PlaceHolder");
      Cursor = content.Load<Texture2D>("Textures/Cursor");
      Rectangle = content.Load<Texture2D>("Textures/Rectangle");
      LightMask = content.Load<Texture2D>("Textures/Lightmask");

      // Shaders
      LightEffect = content.Load<Effect>("Effects/Light");
      LightingEffect = content.Load<Effect>("Effects/Lighting");

      // Fonts
      testFont = content.Load<SpriteFont>("Fonts/Font");

      // Sounds
      Click = content.Load<SoundEffect>("Sounds/Click");
      Cut = content.Load<SoundEffect>("Sounds/Cut");
      Swoosh = content.Load<SoundEffect>("Sounds/Swoosh");
    }
  }
}
