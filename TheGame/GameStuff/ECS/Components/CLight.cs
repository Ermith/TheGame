using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GameStuff.ECS.Components
{
  class CLight : Component
  {
    public bool Active;
    public float Intensity = 1;
    public float X;
    public float Y;
  }
}
