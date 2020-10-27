using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class AISystem : System
  {
    private List<Entity> AIEntities;
    public AISystem(List<Entity> aIEntities)
    {
      AIEntities = aIEntities;
    }

    public override void Update(GameTime time)
    {
      foreach (Entity entity in AIEntities)
      {
        CAI ai = entity.Get<CAI>();

        // Do Something
      }
    }

    private Queue<Vector2> PathFinding(Vector2 pos)
    {
      var path = new Queue<Vector2>();

      return path;
    }
  }
}
