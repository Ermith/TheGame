

namespace CaveGenerator
{
  public class FloorTile : TileType
  {
    public FloorTile() : base()
    {
    }

  }

  public class RockTile : TileType
  {
    public RockTile() : base()
    {
    }

    public override bool Solid => true;
  }

  public class WallTile : TileType
  {
    public WallTile() : base()
    {
    }

    public override bool Solid => true;
  }

  public class MossTile : TileType
  {
    public MossTile() : base()
    {
    }
  }


  public class MossFloorTile : TileType
  {
    public MossFloorTile() : base()
    {
    }

  }


  public class MushroomTile : TileType
  {
    public MushroomTile() : base()
    {
    }
  }

  public class MushroomFloorTile : TileType
  {
    public MushroomFloorTile() : base()
    {

    }
  }

  public class IceTile : TileType
  {
    public IceTile() : base()
    {

    }
  }

  public class IceFloorTile : TileType
  {
    public IceFloorTile() : base()
    {

    }
  }

  public class MagmaTile : TileType
  {
    public MagmaTile() : base()
    {

    }

    public override bool Deadly => true;
  }

  public class MagmaFloorTile : TileType
  {
    public MagmaFloorTile() : base()
    {

    }
  }

  public class FleshTile : TileType
  {
    public FleshTile() : base()
    {

    }

    public override bool Deadly => true;
  }

  public class FleshFloorTile : TileType
  {
    public FleshFloorTile() : base()
    {

    }
  }
}
