using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GameStuff.ECS.Components
{
  enum Direction { Up, Left, Down, Right }
  enum State { Attacking, Standing, Moving, Sneaking, Crouching, AttackWindup, AttackFinish }
  class CBehavior : Component
  {
    public Direction Direction;
    public State State;
    public State LastState;
  }
}
