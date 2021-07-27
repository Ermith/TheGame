﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TheGame.GameStuff.ECS;
using TheGame.GameStuff.ECS.Components;

namespace TheGame
{
  static class Assets
  {
    public static Texture2D placeHolder;
    public static SpriteFont testFont;
    public static SoundEffect Click;

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
    public static Texture2D LightMask;

    public static Texture2D KnightSprite;
    public static Texture2D RogueSprite;
    public static Texture2D[] FireOverlay;
    public static Texture2D[] ShadowOverlay;

    public static CAnimation RogueAnimataion;
    public static CAnimation KnightAnimation;

    public static Effect LightEffect;
    public static Effect LightingEffect;

    static public void Load(ContentManager content)
    {
      placeHolder = content.Load<Texture2D>("placeHolder");
      testFont = content.Load<SpriteFont>("Font");
      Click = content.Load<SoundEffect>("Click");

      FloorTileTexture = content.Load<Texture2D>("Tiles\\FloorTile");
      RockTileTexture = content.Load<Texture2D>("Tiles\\RockTile");
      WallTileTexture = content.Load<Texture2D>("Tiles\\WallTile");
      MossTileTexture = content.Load<Texture2D>("Tiles\\MossTile");
      MossFloorTileTexture = content.Load<Texture2D>("Tiles\\MossFloorTile");
      MushroomTileTexture = content.Load<Texture2D>("Tiles\\MushroomTile");
      MushroomFloorTileTexture = content.Load<Texture2D>("Tiles\\MushroomFloorTile");
      IceTileTexture = content.Load<Texture2D>("Tiles\\IceTile");
      IceFloorTileTexture = content.Load<Texture2D>("Tiles\\IceFloorTile");
      MagmaTileTexture = content.Load<Texture2D>("Tiles\\MagmaTile");
      MagmaFloorTileTexture = content.Load<Texture2D>("Tiles\\MagmaFloorTile");
      FleshTileTexture = content.Load<Texture2D>("Tiles\\FleshTile");
      FleshFloorTileTexture = content.Load<Texture2D>("Tiles\\FleshFloorTile");

      KnightSprite = content.Load<Texture2D>("KnightSprite");
      KnightAnimation = AnimationParser.ParseSettings("KnightInfo.txt", KnightSprite);
      
      RogueSprite = content.Load<Texture2D>("RogueSpriteSheet");
      RogueAnimataion = AnimationParser.ParseSettings("RogueInfo.txt", RogueSprite);

      FireOverlay = new Texture2D[46];
      for (int i = 0; i < 46; i++)
        FireOverlay[i] = content.Load<Texture2D>($"Fire/fire_{i}");

      ShadowOverlay = new Texture2D[46];
      for (int i = 0; i < 46; i++)
        ShadowOverlay[i] = content.Load<Texture2D>($"Shadow/shadow_{i}");

      LightEffect = content.Load<Effect>("Light");
      LightingEffect = content.Load<Effect>("Lighting");
      LightMask = content.Load<Texture2D>("lightmask");
    }
  }
}
