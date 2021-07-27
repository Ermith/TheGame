using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class HealthSystem : System
  {
    List<Entity> healthEntities;
    List<Entity> toDestroy;
    ComponentTracker tracker;

    public HealthSystem(ComponentTracker tracker)
    {
      this.tracker = tracker;
      healthEntities = tracker.GetEntities<CHealth>();
      toDestroy = new List<Entity>();
    }

    public override void Update(GameTime time)
    {
      foreach (Entity e in healthEntities)
      {
        var h = e.Get<CHealth>();
        if (h.HealthPoints <= 0)
          toDestroy.Add(e);
      }

      foreach (Entity e in toDestroy)
      {
        tracker.Destroy(e);
      }

      toDestroy.Clear();

    }
  }
}
