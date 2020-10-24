using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.GameStuff.Components
{
  abstract class Component
  {
    public enum Components { Location, Movement, Render, Input }
  }
}
