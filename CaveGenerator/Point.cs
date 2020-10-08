namespace CaveGenerator {
  internal struct Point {
    public Point(int X, int Y) {
      this.X = X;
      this.Y = Y;
    }

    public int X;
    public int Y;

    public static bool operator!=(Point a, Point b)
    {
      return (a.X != b.X) || (a.Y != b.Y);
    }
    public static bool operator==(Point a, Point b)
    {
      return (a.X == b.X) && (a.Y == b.Y);
    }

    public override bool Equals(object obj)
    {
      if (obj is Point blob)
        return this == blob;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return X.GetHashCode() + Y.GetHashCode();
    }
  }
}