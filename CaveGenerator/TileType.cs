using System.Drawing;

namespace CaveGenerator {
  public abstract class TileType {
    // Static crap
    public static int TILE_WIDTH { get => 32; }
    public static int TILE_HEIGHT { get => 32; }
    public static TileType Floor { get; } = new FloorTile();
    public static TileType Wall { get; } = new WallTile();
    public static TileType Rock { get; } = new RockTile();
    public static TileType Moss { get; } = new MossTile();
    public static TileType MossFloor { get; } = new MossFloorTile();
    public static TileType Ice { get; } = new IceTile();
    public static TileType IceFloor { get; } = new IceFloorTile();
    public static TileType Mushroom { get; } = new MushroomTile();
    public static TileType MushroomFloor { get; } = new MushroomFloorTile();
    public static TileType Magma { get; } = new MagmaTile();
    public static TileType MagmaFloor { get; } = new MagmaFloorTile();
    public static TileType Flesh { get; } = new FleshTile();
    public static TileType FleshFloor { get; } = new FleshFloorTile();


    // Specific crap
    public virtual bool Solid { get => false; }
    public virtual bool Deadly { get => false; }

    public TileType() {
    }

  }
}
