﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.GameStuff.ECS.Components;
using TheGame.Math;

namespace TheGame.GameStuff.ECS.Systems
{
  class AttackSystem : System
  {
    List<Entity> attackEntities;
    List<Entity> healthEntities;

    public AttackSystem(List<Entity> attackEntities, List<Entity> healthEntities)
    {
      this.attackEntities = attackEntities;
      this.healthEntities = healthEntities;
    }



    public override void Update(GameTime time)
    {
      foreach (Entity entity in attackEntities)
      {
        var attack = entity.Get<CAttack>();
        var spacial = entity.Get<CSpacial>();
        var animation = entity.Get<CAnimation>();
        var behavior = entity.Get<CBehavior>();
        Rectangle hit = attack.hitBox;
        hit.Location = spacial.HitBox.Location;
        hit.Location += CommonVectors.GetDirection(animation.dir).ToPoint() * hit.Size;

        if (behavior.State == State.Attacking && attack.attackFrames.Contains(animation.Index))
        {
          foreach (Entity victim in healthEntities)
          {
            if (!attack.attackedEntities.Contains(victim) && victim.Get<CSpacial>().HitBox.Intersects(hit))
            {
              victim.Get<CHealth>().HealthPoints -= attack.damage;
              attack.attackedEntities.Add(victim);
            }
          }
        } else
        {
          attack.attackedEntities.Clear();
        }

      }
    }
  }
}
