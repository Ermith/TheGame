﻿using Microsoft.Xna.Framework;
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
      float t = (float)time.ElapsedGameTime.TotalMilliseconds;
      foreach (Entity entity in animationEntities)
      {
        var animation = entity.Get<CAnimation>();
        UpdateAnimation(animation, t);
      }
    }

    private void UpdateAnimation(CAnimation animation, float time)
    {
      if (animation.finished)
        return;

      animation.Delta += time;
      animation.AnimationTime += time;

      if (animation.Delta > animation.Frequency)
      {
        animation.Index = (animation.Index + 1);
        if (animation.loop)
          animation.Index %= animation.FrameCount;
        else if (animation.Index >= animation.FrameCount)
        {
          animation.finished = true;
          animation.Index = animation.FrameCount - 1;
        }
        animation.Delta -= animation.Frequency;
      }
    }
  }
}
