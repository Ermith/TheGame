using TheGame.GameStuff.Components;
using System.Collections.Generic;

namespace TheGame.GameStuff.Entities
{
  class Entity
  {
    public Dictionary<Component.Components, Component> Components = new Dictionary<Component.Components, Component>();
  }
}
