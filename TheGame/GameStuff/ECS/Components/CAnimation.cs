using Microsoft.Xna.Framework.Graphics;
using System;

namespace TheGame.GameStuff.ECS.Components
{
  static class AnimationEffects
  {
    public static void Empty(CAnimation anim, float t) { }
    public static void FadeIn(CAnimation anim, float t)
    {
      anim.Opacity = t;
    }

    public static void FadeOut(CAnimation anim, float t)
    {
      anim.Opacity = 1 - t;
    }
  }
  enum AnimtaionState { Stopped, Starting, Playing, Ending, Inactive }
  enum AnimationSource { Frames, Sprite }
  class CAnimation : Component
  {
    // necessary
    public float Delta = 0f;
    public int Index = 0;
    public float Frequency = 0.5f;
    public float StartupTime = 0f;
    public float EndTime = 0f;
    public float AnimationTime = 0f;
    public float Opacity = 1f;
    public bool StaticEnding = true;
    public bool StaticStart = false;
    public Action<CAnimation, float> startingEffect = AnimationEffects.Empty;
    public Action<CAnimation, float> endingEffect = AnimationEffects.Empty;
    public Action<CAnimation, float> playingEffect = AnimationEffects.Empty;
    public AnimtaionState State = AnimtaionState.Stopped;
    public AnimationSource source;

    // Frames specific
    public Texture2D[] Frames;

    // Sprite Specific
    public int Row = 0;
    public int Height = 0;
    public int Width = 0;
    public int FrameCount = 0;
    public int DefaultFrame = 0;
    public Texture2D Sprite;
  }
}
