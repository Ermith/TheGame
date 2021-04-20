﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Math;
using Microsoft.Xna.Framework.Graphics;
using TheGame.GameStuff.ECS.Components;
using System;

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

    public ComponentBuilder Spacial(Vector2 position, Direction facing = Direction.Up, int wid = 28, int hei = 28)
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
        Sprite = sprite,
        DefaultFrame = defaultFrame,
        FrameCount = framecount,
        Width = wid,
        Height = hei,
        Frequency = frequency,
        source = AnimationSource.Sprite,
        StartupTime = startupTime,
        EndTime = endTime,
        endingEffect = endingEffect,
        startingEffect = startingEffect,
        Opacity = opacity,
        frameCoords = new System.Collections.Generic.Dictionary<State, (int, int)>(),
        frameCounts = new System.Collections.Generic.Dictionary<State, int>()
      };

      anim.frameCoords[State.Moving] = (0, 0);
      anim.frameCoords[State.Standing] = (0, 0);
      anim.frameCoords[State.Sneaking] = (0, 4);
      anim.frameCoords[State.Crouching] = (0, 4);
      anim.frameCoords[State.AttackWindup] = (0, 5);
      anim.frameCoords[State.Attacking] = (9, 5);

      anim.frameCounts[State.Moving] = 14;
      anim.frameCounts[State.Standing] = 1;
      anim.frameCounts[State.Sneaking] = 14;
      anim.frameCounts[State.Crouching] = 1;
      anim.frameCounts[State.AttackWindup] = 9;
      anim.frameCounts[State.Attacking] = 5;

      tracker.Add(Target, anim);
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
        Frames = frames,
        FrameCount = frames.Length,
        Frequency = frequency,
        Opacity = opacity,
        StartupTime = startupTime,
        EndTime = endTime,
        source = AnimationSource.Frames
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
  }
}
