﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class AnimationSystem : System, IRenderable
  {
    private List<Entity> animationEntities;

    public AnimationSystem(List<Entity> animationEntities)
    {
      this.animationEntities = animationEntities;
    }

    public void Render(SpriteBatch batch)
    {
      foreach (Entity entity in animationEntities)
      {
        CAnimation render = entity.Get<CAnimation>();
        CSpacial spacial = entity.Get<CSpacial>();

        Vector2 relativePosition = Camera.AbsoluteToRelative(spacial.Position);

        batch.Draw(render.Sprite, relativePosition, 
          new Rectangle(
            render.CurrentFrameIndex * 32,
            render.Heigth * (int)spacial.Facing,
            render.Width, render.Heigth),
          Color.White);
      }
    }

    public override void Update(GameTime time)
    {
      foreach (Entity entity in animationEntities)
      {
        CAnimation animation = entity.Get<CAnimation>();
        CMovement movement = entity.Get<CMovement>();
        if (movement != null && movement.Velocity.X == 0 && movement.Velocity.Y == 0)
        {
          animation.Delta = 0;
          animation.CurrentFrameIndex = animation.DefaultFrame;
          continue;
        }

        animation.Delta += (float)time.ElapsedGameTime.TotalMilliseconds;

        if (animation.Delta >= animation.Frequency)
        {
          animation.CurrentFrameIndex = (animation.CurrentFrameIndex + 1) % animation.FrameCount;
          animation.Delta -= animation.Frequency;
        }
      }
    }

  }
}
