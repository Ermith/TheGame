using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MapEditor
{
  static class Assets
  {
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

    public static void Load(GraphicsDevice graphics)
    {
      string contentFile = "./TheGame/Content";
      FloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/FloorTile.png");
      RockTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/RockTile.png");
      WallTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/WallTile.png");
      MossTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MossTile.png");
      MossFloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MossFloorTile.png");
      MushroomTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MushroomTile.png");
      MushroomFloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MushroomFloorTile.png");
      IceTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/IceTile.png");
      IceFloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/IceFloorTile.png");
      MagmaTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MagmaTile.png");
      MagmaFloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/MagmaFloorTile.png");
      FleshTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/FleshTile.png");
      FleshFloorTileTexture = Texture2D.FromFile(graphics, $"{contentFile}/Textures/Tiles/FleshFloorTile.png");
    }
  }
}
