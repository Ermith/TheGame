using System;
using System.Collections.Generic;


namespace CaveGenerator
{
  public class MapGenerator
  {
    public int BlockWidth { get; }
    public int BlockHeight { get; }
    public int Blocks { get; }
    public int Width { get => BlockWidth * Blocks; }
    public int Height { get => BlockHeight * Blocks; }

    public int caIterations = 3;
    public int mooreNeighborhood = 2;
    public int rockTreshold = 17;
    public int rockPercentage = 50;
    public int[,] CompArr { get; private set; }
    public int Comps { get; private set; }
    private Random rnd;

    public MapGenerator(int blockWidth, int blockHeight, int blocks)
    {
      BlockWidth = blockWidth;
      BlockHeight = blockHeight;
      Blocks = blocks;
      rnd = new Random();
    }

    public MapGenerator(int blockWidth, int blockHeight, int blocks, int seed)
    {
      BlockWidth = blockWidth;
      BlockHeight = blockHeight;
      Blocks = blocks;
      rnd = new Random(seed);
    }

    private TileType[,] init()
    {
      TileType[,] map = new TileType[Width, Height];

      for (int j = 0; j < Height; j++)
      {
        for (int i = 0; i < Width; i++)
        {
          map[i, j] = TileType.Floor;
        }
      }

      return map;
    }

    private void Lay(TileType[,] map, TileType tile, TileType secondary, int percentage)
    {

      for (int j = 0; j < Height; j++)
      {
        for (int i = 0; i < Width; i++)
        {

          if (map[i, j] == TileType.Floor)
          {
            if (rnd.Next(100) < percentage)
              map[i, j] = tile;
            else if (rnd.Next(100) < percentage)
              map[i, j] = secondary;
          }
        }
      }
    }

    private bool CheckBounds(int x, int y) => (x >= 0 && x < Width && y >= 0 && y < Height);

    private int[,] getNeighbors(TileType[,] map, TileType tile, int neighborhood)
    {
      int[,] neighbors = new int[Width, Height];
      int nb;

      for (int j = 0; j < Height; j++)
      {
        for (int i = 0; i < Width; i++)
        {

          nb = 0;
          if (map[i, j] == tile)
            nb--;

          for (int a = -neighborhood; a <= neighborhood; a++)
          {
            for (int b = -neighborhood; b <= neighborhood; b++)
            {

              if (CheckBounds(i + a, j + b))
              {
                if (map[i + a, j + b] == tile)
                  nb++;
              }
              else
              {
                nb += rockTreshold;
              }
            }
          }

          neighbors[i, j] = nb;
        }
      }

      return neighbors;
    }

    private void CA(TileType[,] map, TileType tile, TileType secondary, int neighborhood, int treshold)
    {
      int[,] neighbors = getNeighbors(map, tile, neighborhood);

      for (int j = 0; j < Height; j++)
      {
        for (int i = 0; i < Width; i++)
        {

          if (neighbors[i, j] >= treshold)
            map[i, j] = tile;
          else if (map[i, j] == tile)
            map[i, j] = secondary;
        }
      }
    }

    private void Lay(TileType[,] map, int x, int y, TileType primary, TileType secondary, int percentage)
    {
      x *= BlockWidth;
      y *= BlockHeight;


      for (int j = y; j < BlockHeight + y; j++)
      {
        for (int i = x; i < BlockWidth + x; i++)
        {

          if (map[i, j] == TileType.Floor)
          {
            if (rnd.Next(100) < percentage)
              map[i, j] = primary;
            else if (rnd.Next(100) < percentage)
              map[i, j] = secondary;

            if (map[i, j] == TileType.Floor && secondary == TileType.FleshFloor)
              map[i, j] = TileType.FleshFloor;
          }
        }
      }
    }

    private int[,] getNeighbors(TileType[,] map, int x, int y, TileType tile, int neighborhood)
    {
      int[,] neighbors = new int[BlockWidth, BlockHeight];
      x *= BlockWidth;
      y *= BlockHeight;

      int nb;

      for (int j = 0; j < BlockHeight; j++)
      {
        for (int i = 0; i < BlockWidth; i++)
        {

          nb = 0;
          if (map[x + i, y + j] == tile)
            nb--;

          for (int a = -neighborhood; a <= neighborhood; a++)
          {
            for (int b = -neighborhood; b <= neighborhood; b++)
            {

              if (CheckBounds(x + i + a, y + j + b))
              {
                if (map[x + i + a, y + j + b] == tile)
                  nb++;
              }
            }
          }

          neighbors[i, j] = nb;

        }
      }

      return neighbors;
    }

    private void CA(TileType[,] map, int x, int y, TileType primary, TileType secondary, int neighborhood, int treshold)
    {
      int[,] neighbors = getNeighbors(map, x, y, primary, neighborhood);

      x *= BlockWidth;
      y *= BlockHeight;

      for (int j = 0; j < BlockHeight; j++)
      {
        for (int i = 0; i < BlockWidth; i++)
        {

          if (neighbors[i, j] >= treshold)
            map[x + i, y + j] = primary;
          else if (map[x + i, y + j] == primary)
            map[x + i, y + j] = secondary;
        }
      }
    }

    public void Fill(TileType[,] Map, int x, int y, GeneratorOption option)
    {
      Lay(Map, x, y, option.PrimaryTile, option.SecondaryTile, option.Percentage);

      for (int i = 0; i < option.CAIterations; i++)
      {
        CA(Map, x, y, option.PrimaryTile, option.SecondaryTile, option.MooreNeighborhood, option.Treshold);
      }
    }

    public void LayWalls(TileType[,] map)
    {
      bool neighbor;

      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {

          if (map[i, j] == TileType.Rock)
          {
            neighbor = false;

            for (int a = -1; a <= 1; a++)
            {
              for (int b = -1; b <= 1; b++)
              {

                if (CheckBounds(i + a, j + b))
                {
                  if (map[i + a, j + b] != TileType.Rock
                      && map[i + a, j + b] != TileType.Wall)

                    neighbor = true;
                }

              }
            }

            if (neighbor)
              map[i, j] = TileType.Wall;
          }
          else
          {
            if (i == 0 || i == Width - 1 || j == 0 || j == Height - 1)
              map[i, j] = TileType.Wall;
          }


        }
      }
    }

    private void BFS(TileType[,] map, int x, int y, int component, ref int[,] compArr)
    {
      var points = new Queue<Point>();
      compArr[x, y] = component;
      points.Enqueue(new Point(x, y));
      Point pt;
      TileType tile;

      while (points.Count > 0)
      {
        pt = points.Dequeue();

        for (int i = -1; i <= 1; i++)
        {
          if (CheckBounds(pt.X + i, pt.Y))
          {

            tile = map[pt.X + i, pt.Y];

            if (compArr[pt.X + i, pt.Y] == 0 &&
                tile != TileType.Rock &&
                tile != TileType.Wall)
            {

              compArr[pt.X + i, pt.Y] = component;
              points.Enqueue(new Point(pt.X + i, pt.Y));
            }
          }
        }

        for (int i = -1; i <= 1; i++)
        {
          if (CheckBounds(pt.X, pt.Y + i))
          {

            tile = map[pt.X, pt.Y + i];

            if (compArr[pt.X, pt.Y + i] == 0 &&
                tile != TileType.Rock &&
                tile != TileType.Wall)
            {

              compArr[pt.X, pt.Y + i] = component;
              points.Enqueue(new Point(pt.X, pt.Y + i));
            }
          }
        }
      }
    }

    private Stack<Point> Closest(TileType[,] map, Point start, ref bool[] contains, int[,] compArr)
    {
      var queue = new Queue<Point>();
      var visited = new HashSet<Point>();
      var parent = new Dictionary<Point, Point>();

      TileType tile = map[start.X, start.Y];
      contains[compArr[start.X, start.Y]] = true;
      queue.Enqueue(start);
      visited.Add(start);
      parent.Add(start, start);
      Point pt, neighbor;

      while (queue.Count > 0)
      {
        pt = queue.Dequeue();

        for (int i = -1; i <= 1; i++)
        {
          for (int j = -1; j <= 1; j++)
          {

            if (CheckBounds(pt.X + i, pt.Y + j))
            {
              tile = map[pt.X + i, pt.Y + j];
              neighbor = new Point(pt.X + i, pt.Y + j);

              if (!visited.Contains(neighbor))
              {
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
                parent.Add(neighbor, pt);
              }

              if (tile != TileType.Rock && !contains[compArr[pt.X + i, pt.Y + j]])
              {
                Stack<Point> path = new Stack<Point>();

                while (neighbor != parent[neighbor])
                {
                  path.Push(neighbor);
                  neighbor = parent[neighbor];
                }

                return path;
              }
            }
          }
        }
      }

      return new Stack<Point>();
    }


    private void Paths(TileType[,] map, int components, int[,] compArr)
    {
      TileType tile, neighbor;
      bool[] contains = new bool[components + 1];
      Stack<Point> path;
      int pathWidth = 1;
      Point pt;

      for (int i = 0; i < components; i++)
      {
        contains[i] = false;
      }

      contains[0] = true;

      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {
          tile = map[i, j];

          if (tile != TileType.Rock && !contains[compArr[i, j]])
          {

            contains[compArr[i, j]] = true;
            path = Closest(map, new Point(i, j), ref contains, compArr);

            while (path.Count > 0)
            {
              pt = path.Pop();

              for (int a = -pathWidth; a <= pathWidth; a++)
              {
                for (int b = -pathWidth; b <= pathWidth; b++)
                {

                  if (CheckBounds(pt.X + a, pt.Y + b))
                  {
                    neighbor = map[pt.X + a, pt.Y + b];

                    if (neighbor == TileType.Rock)
                    {
                      compArr[pt.X + a, pt.Y + b] = compArr[i, j];
                      map[pt.X + a, pt.Y + b] = TileType.Floor;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    private void ConnectComponents(TileType[,] map)
    {
      int[,] compArr = new int[Width, Height];
      int component = 1;

      for (int i = 0; i < Width; i++)
      {
        for (int j = 0; j < Height; j++)
        {
          if (map[i, j] != TileType.Rock
              && map[i, j] != TileType.Wall
                  && compArr[i, j] == 0)
          {

            BFS(map, i, j, component, ref compArr);
            component++;
          }
        }
      }

      CompArr = (int[,])compArr.Clone();

      Paths(map, component - 1, compArr);
      Comps = component;
    }
    public TileType[,] Generate(GeneratorOption basicRockOption)
    {
      rockPercentage = basicRockOption.Percentage;
      mooreNeighborhood = basicRockOption.MooreNeighborhood;
      rockTreshold = basicRockOption.Treshold;
      caIterations = basicRockOption.CAIterations;

      TileType[,] Map = init();

      Lay(Map, TileType.Rock, TileType.Floor, rockPercentage);

      for (int i = 0; i < caIterations; i++)
      {
        CA(Map, TileType.Rock, TileType.Floor, mooreNeighborhood, rockTreshold);
      }

      ConnectComponents(Map);

      return Map;
    }
  }
}
