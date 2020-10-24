using TheGame.GameStuff.ECS.Components;
using System.Collections.Generic;

namespace TheGame.GameStuff.ECS
{
  class Entity
  {
    public Dictionary<ComponentTypes, Component> Components = new Dictionary<ComponentTypes, Component>();
  }
}
