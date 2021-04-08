using System;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS
{
  class ComponentTracker
  {
    private readonly Dictionary<Type, List<Entity>> data = new Dictionary<Type, List<Entity>>();

    public void Add<T>(Entity e, T comp) where T : Component
    {
      if (!data.ContainsKey(typeof(T)))
        data.Add(typeof(T), new List<Entity>());
      
      if (data[typeof(T)].Contains(e))
        return;

      data[typeof(T)].Add(e);
      e.Add(comp);
    }

    public void Remove<T>(Entity e) where T : Component
    {
      if (!data.ContainsKey(typeof(T)))
        return;

      if (!data[typeof(T)].Contains(e))
        return;

      data[typeof(T)].Remove(e);
      e.Remove<T>();
    }

    public List<Entity> GetEntities<T>()
    {
      return data.ContainsKey(typeof(T)) ? data[typeof(T)] : null;
    }
  }
}
