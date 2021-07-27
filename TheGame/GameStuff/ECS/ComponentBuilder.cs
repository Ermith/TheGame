using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Math;
using Microsoft.Xna.Framework.Graphics;
using TheGame.GameStuff.ECS.Components;
using System;
using System.Collections.Generic;

namespace TheGame.GameStuff.ECS
{
  class ComponentBuilder
  {
    public Entity Target { get; set; }
    private readonly ComponentTracker tracker;

    public ComponentBuilder(ComponentTracker tracker, Entity target = null)
    {
      this.tracker = tracker;
      Target = target;
    }

    public ComponentBuilder Spacial(Vector2 position, Direction facing = Direction.Up, int wid = 16, int hei = 28)
    {
      CSpacial spacial = new CSpacial
      {
        Position = position,
        Width = wid,
        Height = hei
      };

      tracker.Add(Target, spacial);

      return this;
    }

    public ComponentBuilder Movement(float speed = 0.2f)
    {
      CMovement movement = new CMovement
      {
        Speed = speed,
        Velocity = Vector2.Zero
      };

      tracker.Add(Target, movement);
      return this;
    }

    public ComponentBuilder Input(
      Keys up = Keys.W,
      Keys left = Keys.A,
      Keys down = Keys.S,
      Keys right = Keys.D,
      Keys attack = Keys.Space)
    {
      CInput input = new CInput
      {
        Up = up,
        Left = left,
        Down = down,
        Right = right,
        Attack = attack
      };

      tracker.Add(Target, input);
      return this;
    }

    public ComponentBuilder Animation(
      Texture2D sprite,
      int framecount,
      int wid,
      int hei,
      float frequency = 100,
      int defaultFrame = 0,
      float opacity = 1f,
      float startupTime = 0f,
      float endTime = 0f,
      Action<CAnimation, float> startingEffect = null,
      Action<CAnimation, float> endingEffect = null
      )
    {
      CAnimation anim = new CAnimation
      {
        SpriteSheet = sprite,
        DefaultFrame = defaultFrame,
        FrameCount = framecount,
        Width = wid,
        Height = hei,
        Frequency = frequency,
        StartupTime = startupTime,
        EndTime = endTime,
        Opacity = opacity,
        frameCoords = new System.Collections.Generic.Dictionary<State, (int, int)>(),
        frameCounts = new System.Collections.Generic.Dictionary<State, int>()
      };

      anim.frameCoords[State.Moving] = (0, 0);
      anim.frameCoords[State.Standing] = (0, 0);
      anim.frameCoords[State.Sneaking] = (0, 4);
      anim.frameCoords[State.Crouching] = (0, 4);
      anim.frameCoords[State.AttackWindup] = (0, 8);
      anim.frameCoords[State.Attacking] = (9, 8);

      anim.frameCounts[State.Moving] = 14;
      anim.frameCounts[State.Standing] = 1;
      anim.frameCounts[State.Sneaking] = 14;
      anim.frameCounts[State.Crouching] = 1;
      anim.frameCounts[State.AttackWindup] = 9;
      anim.frameCounts[State.Attacking] = 5;

      tracker.Add(Target, anim);
      return this;
    }

    public ComponentBuilder Animation(CAnimation anim)
    {
      CAnimation newAnim = new CAnimation
      {
        Delta = anim.Delta,
        Index = anim.Index,
        Frequency = anim.Frequency,
        StartupTime = anim.StartupTime,
        EndTime = anim.EndTime,
        AnimationTime = anim.AnimationTime,
        Opacity = anim.Opacity,
        StaticEnding = anim.StaticEnding,
        StaticStart = anim.StaticStart,
        loop = anim.loop,
        finished = anim.finished,
        State = anim.State,
        BehaviorState = anim.BehaviorState,
        dir = anim.dir,
        frameCounts = anim.frameCounts,
        X = anim.X,
        Y = anim.Y,
        Height = anim.Height,
        Width = anim.Width,
        FrameCount = anim.FrameCount,
        DefaultFrame = anim.DefaultFrame,
        SpriteSheet = anim.SpriteSheet,
        frameCoords = anim.frameCoords
      };

      tracker.Add(Target, newAnim);
      return this;
    } 

    public ComponentBuilder Animation(
      Texture2D[] frames,
      float frequency,
      float opacity = 0f,
      float startupTime = 0f,
      float endTime = 0f
      )
    {
      CAnimation anim = new CAnimation
      {
        FrameCount = frames.Length,
        Frequency = frequency,
        Opacity = opacity,
        StartupTime = startupTime,
        EndTime = endTime
      };

      tracker.Add(Target, anim);
      return this;
    }

    public ComponentBuilder Behavior()
    {
      var behavior = new CBehavior();

      behavior.Direction = Direction.Up;
      behavior.State = State.Moving;

      tracker.Add(Target, behavior);
      return this;
    }

    public ComponentBuilder Light(float intensity = 1)
    {
      var light = new CLight();
      light.Intensity = intensity;

      tracker.Add(Target, light);
      return this;
    }

    public ComponentBuilder Health(int hp = 100)
    {
      var health = new CHealth();
      health.HealthPoints = hp;

      tracker.Add(Target, health);
      return this;
    }

    public ComponentBuilder Attack(int dmg = 50)
    {
      var attack = new CAttack();
      attack.damage = dmg;
      attack.attackWidth = 32;
      attack.attackHeight = 32;
      attack.attackedEntities = new HashSet<Entity>();

      tracker.Add(Target, attack);
      return this;
    }
  }
}
