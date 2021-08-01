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
    public int CurrentAttack;
    public List<int> Damages;
    public List<(int, int)> Sizes;
    public List<bool> Chargables;
    public int AttacksCount => Damages.Count;
    public Rectangle HitBox => new Rectangle(0, 0, Sizes[CurrentAttack].Item1, Sizes[CurrentAttack].Item2);
    public int Damage => Damages[CurrentAttack];
    public bool Chargable => Chargables[CurrentAttack];
    public HashSet<Entity> attackedEntities;
  }
}
