using System;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS
{
  class ComponentTracker
  {
    private Dictionary<Type, List<Entity>> data = new Dictionary<Type, List<Entity>>();

    public void Add<T>(Entity e, T comp) where T : Component
    {
      if (!data.ContainsKey(typeof(T)))
        data.Add(typeof(T), new List<Entity>());
      
      if (data[typeof(T)].Contains(e))
        return;

      data[typeof(T)].Add(e);
    }

    public List<Entity> GetEntities<T>()
    {
      return data.ContainsKey(typeof(T)) ? data[typeof(T)] : null;
    }
  }
}
