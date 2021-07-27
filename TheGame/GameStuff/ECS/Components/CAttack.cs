using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GameStuff.ECS.Components
{
  class CAttack : Component
  {
    public int[] attackFrames = { 2, 3, 4};
    public int damage;
    public int attackWidth = 32;
    public int attackHeight = 32;

    public Rectangle hitBox => new Rectangle(0, 0, attackWidth, attackHeight);
    public HashSet<Entity> attackedEntities;
  }
}
