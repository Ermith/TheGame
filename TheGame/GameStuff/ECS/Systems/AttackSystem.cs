using Microsoft.Xna.Framework;
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
        Rectangle hit = attack.HitBox;


        // Center on center
        var loc = new Vector2(
          spacial.X - hit.Width / 2,
          spacial.Y - hit.Height / 2
        );

        // offset
        var offset = new Vector2(
          spacial.Width / 2 + hit.Width / 2,
          spacial.Height / 2 + hit.Height / 2
        );

        hit.Location = loc.ToPoint() + CommonVectors.GetDirection(animation.dir).ToPoint() * offset.ToPoint();

        
        if (behavior.State == State.Attacking)
        {
          foreach (Entity victim in healthEntities)
          {
            if (!attack.attackedEntities.Contains(victim) && victim.Get<CSpacial>().HitBox.Intersects(hit))
            {
              victim.Get<CHealth>().HealthPoints -= attack.Damage;
              attack.attackedEntities.Add(victim);
              Assets.Cut.Play();
            }
          }
        }
        else
        {
          attack.attackedEntities.Clear();
        }

      }
    }
  }
}
