using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheGame.GameStuff.ECS.Components
{
  class CMovement : Component
  {
    public Vector2 Velocity { get; set; }
    public float Speed { get; set; }
  }
}
