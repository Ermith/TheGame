using System.Collections.Generic;
using TheGame.GameStuff.ECS;
using Microsoft.Xna.Framework.Input;

namespace TheGame.GameStuff.ECS.Components
{
  class CInput : Component
  {
    // Controls
    public Keys Up = Keys.W;
    public Keys Left = Keys.A;
    public Keys Down = Keys.S;
    public Keys Right = Keys.D;
    public Keys Attack = Keys.Space;
  }
}
