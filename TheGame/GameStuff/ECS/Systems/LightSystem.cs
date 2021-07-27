using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class LightSystem : System
  {
    List<Entity> lightEntities = new List<Entity>();

    public LightSystem(List<Entity> lightEntities)
    {
      this.lightEntities = lightEntities;
    }

    public override void Update(GameTime time)
    {
      foreach (Entity e in lightEntities)
      {
        CSpacial spacial = e.Get<CSpacial>();
        CLight light = e.Get<CLight>();
        
        if (spacial != null)
        {
          light.X = spacial.X;
          light.Y = spacial.Y;
        }
      }
    }
  }
}
