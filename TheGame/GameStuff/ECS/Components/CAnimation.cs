using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TheGame.GameStuff.ECS.Components
{
  enum AnimtaionState { Stopped, Starting, Playing, Ending, Inactive }
  enum AnimationSource { Frames, SpriteSheet }
  class CAnimation : Component
  {
    public CAnimation()
    {
      frameCoords = new Dictionary<State, (int, int)>();
      frameCounts = new Dictionary<State, int>();
      attackFrameCounts = new List<Dictionary<State, int>>();
      attackFrameCoords = new List<Dictionary<State, (int, int)>>();
    }


    public int Index = 0;
    public int X = 0;
    public int Y = 0;
    public int Height = 32;
    public int Width = 32;
    public int FrameCount = 0;
    public int DefaultFrame = 0;

    public float Delta = 0f;
    public float Frequency = 75f;
    public float StartupTime = 0f;
    public float EndTime = 0f;
    public float AnimationTime = 0f;
    public float Opacity = 1f;

    public bool StaticEnding = true;
    public bool StaticStart = false;
    public bool loop = true;
    public bool finished = false;


    public AnimtaionState State = AnimtaionState.Stopped;
    public State BehaviorState = Components.State.Moving;
    public Direction dir = Direction.Up;
    public Texture2D SpriteSheet;
    public Dictionary<State, int> frameCounts;
    public List<Dictionary<State, int>> attackFrameCounts;
    public Dictionary<State, (int, int)> frameCoords;
    public List<Dictionary<State, (int, int)>> attackFrameCoords;
  }
}
