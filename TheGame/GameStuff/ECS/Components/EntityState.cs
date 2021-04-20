using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.States;

namespace TheGame.GameStuff.ECS.Components
{
  abstract class EntityState
  {
    abstract protected State state { get; }

    public virtual void Enter(CAnimation animation, CBehavior behavior)
    {
      SetAnimationBehavior(animation, state);
      behavior.State = state;
    }

    public virtual void Execute() { }

    public virtual void Leave() { }


    private void SetAnimationBehavior(CAnimation animation, State state)
    {
      animation.Index = 0;
      animation.FrameCount = animation.frameCounts[state];
      (int x, int y) = animation.frameCoords[state];
      animation.X = x;
      animation.Y = y;
    }

    public static EntityState Standing { get; } = new StandingState();

    public static EntityState Moving { get; } = new MovingState();

    public static EntityState Crouching { get; } = new CrouchState();
    public static EntityState Sneaking { get; } = new SneakingState();
  }

  class CrouchState : EntityState
  {
    protected override State state => State.Crouching;

    public override void Enter(CAnimation animation, CBehavior behavior)
    {
      animation.dir = Direction.Up;
      base.Enter(animation, behavior);
      Camera.ZoomEffect(-0.3f);
    }

    public override void Execute()
    {
      base.Execute();
    }

    public override void Leave()
    {
      base.Leave();
      Camera.ZoomStop();
    }
  }

  class StandingState : EntityState
  {
    protected override State state => State.Standing;
  }

  class MovingState : EntityState
  {
    protected override State state => State.Moving;
  }

  class SneakingState : EntityState
  {
    protected override State state => State.Sneaking;

    public override void Enter(CAnimation animation, CBehavior behavior)
    {
      base.Enter(animation, behavior);
      animation.dir = Direction.Up;
      Camera.ZoomEffect(-0.3f);
    }

    public override void Execute()
    {
      base.Execute();
    }

    public override void Leave()
    {
      base.Leave();
      Camera.ZoomStop();
    }
  }
}