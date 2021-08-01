using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GameStuff.ECS.Components
{
  enum Direction { Up, Left, Down, Right }
  enum State {
    // Movement
    Standing,
    Moving,

    // Sneaking
    Sneaking,
    Crouching,

    // Attacks
    AttackWindup,
    Attacking,
    AttackFinish
  }
  class CBehavior : Component
  {
    public static HashSet<State> AttackStates = new HashSet<State>(new State[] { State.AttackWindup, State.Attacking, State.AttackFinish});
    public Direction Direction;
    public State State;
    public State LastState;
  }
}
