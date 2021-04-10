using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class AnimationSystem : System
  {
    private List<Entity> animationEntities;

    public AnimationSystem(List<Entity> animationEntities)
    {
      this.animationEntities = animationEntities;
    }

    public override void Update(GameTime time)
    {
      foreach (Entity entity in animationEntities)
        UpdateAnimation(time, entity.Get<CAnimation>());

      foreach (CAnimation animation in Camera.Overlays)
        UpdateAnimation(time, animation);
    }

    private void UpdateAnimation(GameTime time, CAnimation animation)
    {

      if (animation.State == AnimtaionState.Stopped || animation.State == AnimtaionState.Inactive)
      {
        animation.Index = animation.DefaultFrame;
        return;
      }

      UpdateTime(time, animation);
      HandleState(animation);
    }

    private void HandleState(CAnimation animation)
    {
      if (animation.State == AnimtaionState.Starting)
        AnimationStart(animation);
      else if (animation.State == AnimtaionState.Playing)
        AnimationPlaying(animation);
      else if (animation.State == AnimtaionState.Ending)
        AnimationEnd(animation);
    }

    private void AnimationStart(CAnimation animation)
    {
      if (animation.StartupTime == 0)
      {
        animation.State = AnimtaionState.Playing;
        return;
      }

      if (animation.StaticStart)
        animation.Index = animation.DefaultFrame;

      float t = animation.AnimationTime / animation.StartupTime;
      if (t >= 1f)
      {
        animation.State = AnimtaionState.Playing;
        animation.AnimationTime = 0;
        t = 1f;
      }

      animation.startingEffect(animation, t);
    }

    private void AnimationEnd(CAnimation animation)
    {
      if (animation.EndTime == 0)
      {
        animation.State = AnimtaionState.Stopped;
        return;
      }

      if (animation.StaticEnding)
        animation.Index = animation.DefaultFrame;

      float t = animation.AnimationTime / animation.StartupTime;
      if (t >= 1f)
      {
        animation.State = AnimtaionState.Stopped;
        animation.AnimationTime = 0;
        t = 1f;
      }

      animation.endingEffect(animation, t);
    }

    private void AnimationPlaying(CAnimation animation)
    {

    }

    private void UpdateTime(GameTime time, CAnimation animation)
    {
      animation.Delta += (float)time.ElapsedGameTime.TotalMilliseconds;
      animation.AnimationTime += (float)time.ElapsedGameTime.TotalMilliseconds;

      if (animation.Delta > animation.Frequency)
      {
        animation.Index = (animation.Index + 1) % animation.FrameCount;
        animation.Delta -= animation.Frequency;
      }
    }
  }
}
