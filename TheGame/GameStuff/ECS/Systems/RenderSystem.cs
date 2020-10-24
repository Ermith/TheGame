using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class RenderSystem : System, IRenderable
  {
    public void Render(RenderArguments arguments)
    {
      foreach (Entity entity in CAnimation.entities)
      {
        CAnimation render = entity.Components[ComponentTypes.Animation] as CAnimation;
        CSpacial spacial = entity.Components[ComponentTypes.Spacial] as CSpacial;

        Vector2 relativePosition = Camera.AbsoluteToRelative(spacial.Position);

        arguments.SpriteBatch.Draw(render.Sprite, relativePosition, 
          new Rectangle(
            render.CurrentFrameIndex * 32,
            render.Heigth * (int)spacial.Facing,
            render.Width, render.Heigth),
          Color.White);
      }
    }

    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in CAnimation.entities)
      {
        CAnimation animation = entity.Components[ComponentTypes.Animation] as CAnimation;
        CMovement movement = entity.Components[ComponentTypes.Movement] as CMovement;
        if (movement != null && movement.Velocity.X == 0 && movement.Velocity.Y == 0)
        {
          animation.Delta = 0;
          animation.CurrentFrameIndex = animation.DefaultFrame;
          continue;
        }

        animation.Delta += (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;

        if (animation.Delta >= animation.Frequency)
        {
          animation.CurrentFrameIndex = (animation.CurrentFrameIndex + 1) % animation.FrameCount;
          animation.Delta -= animation.Frequency;
        }
      }
    }

  }
}
