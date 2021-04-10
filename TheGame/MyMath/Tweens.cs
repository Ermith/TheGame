using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.MyMath
{
  static class Tweens
  {
    public static float SmoothStart2(float t) {
      return t * t;
    }

    public static float Reverse(float t)
    {
      return 1 - t;
    }

    public static float SmoothStop2(float t)
    {
      return 1 - (1 - t) * (1 - t);
    }


    public static float SmoothStart4(float t) => t * t * t * t;
    public static float SmoothStop4(float t) => 1 - SmoothStart4(1 - t);

    public static float SmoothStep2(float t)
    {
      return (1 - 0.7f) * SmoothStart2(t) + 0.7f * SmoothStop2(t);
    }
    public static float SmoothStep4(float t)
    {
      return (1 - t) * SmoothStart4(t) + t * SmoothStop4(t);
    }
  }
}
