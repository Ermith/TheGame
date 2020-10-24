using Microsoft.Xna.Framework;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class RenderSystem : System, IRenderable
  {
    public void Render(RenderArguments arguments)
    {
      foreach (Entity entity in CRender.entities)
      {
        CRender render = entity.Components[ComponentTypes.Render] as CRender;
        CLocation location = entity.Components[ComponentTypes.Location] as CLocation;

        Camera.AbsoluteToRelative((int)location.X, (int)location.Y, out int lx, out int ly);

        arguments.SpriteBatch.Draw(render.Sprite, new Vector2(lx, ly), 
          new Rectangle(
            render.CurrentFrameIndex * 32,
            render.Heigth * (int)location.Direction,
            render.Width, render.Heigth),
          Color.White);
      }
    }

    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in CRender.entities)
      {
        CRender render = entity.Components[ComponentTypes.Render] as CRender;
        CMovement movement = entity.Components[ComponentTypes.Movement] as CMovement;
        if (movement != null && movement.dX == 0 && movement.dY == 0)
        {
          render.delta = 0;
          render.CurrentFrameIndex = render.defaultFrame;
          continue;
        }

        render.delta += (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;

        if (render.delta >= render.Frequency)
        {
          render.CurrentFrameIndex = (render.CurrentFrameIndex + 1) % render.FrameCount;
          render.delta -= render.Frequency;
        }
      }
    }

  }
}
