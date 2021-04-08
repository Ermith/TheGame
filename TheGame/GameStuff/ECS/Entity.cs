using TheGame.GameStuff.ECS.Components;
using System.Collections.Generic;
using System;

namespace TheGame.GameStuff.ECS
{
  class Entity
  {
    private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

    public T Get<T>() where T : Component
    {
      if (components.TryGetValue(typeof(T), out Component comp))
        return (T)comp;

      return null;
    }

    public void Add<T>(T component) where T : Component
    {
      if (components.ContainsKey(typeof(T)))
        components[typeof(T)] = component;
      else
        components.Add(typeof(T), component);
    }

    public void Remove<T>() where T : Component
    {
      components.Remove(typeof(T));
    }
  }
}
