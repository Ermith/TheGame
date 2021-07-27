using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class RenderSystem : IRenderable
  {
    private List<Entity> animationEntities;
    private List<Entity> lightEntities;
    private Camera camera;

    public RenderSystem(List<Entity> animationEntities, List<Entity> lightEntities, Camera camera)
    {
      this.animationEntities = animationEntities;
      this.lightEntities = lightEntities;
      this.camera = camera;
    }

    public void Render(SpriteBatch batch)
    {
      foreach (Entity entity in animationEntities)
        RenderAnimation(entity.Get<CAnimation>(), entity.Get<CSpacial>(), batch);
    }

    public void RenderLight(SpriteBatch batch)
    {
      foreach (Entity entity in lightEntities)
      {
        CLight light = entity.Get<CLight>();
        Vector2 position = new Vector2(light.X, light.Y);
        Vector2 scale = new Vector2(camera.ScaleX, camera.ScaleY);
        position = camera.AbsoluteToRelative(position);
        position *= scale;
        position -= Assets.LightMask.Bounds.Center.ToVector2() * light.Intensity * scale ;


        batch.Draw(
          texture: Assets.LightMask,
          position: position,
          sourceRectangle: null,
          color: Color.White,
          rotation: 0,
          origin: Vector2.Zero,
          scale: light.Intensity * scale,
          effects: SpriteEffects.None,
          layerDepth: 0
          );
      }
    }


    private void RenderAnimation(CAnimation animation, CSpacial spacial, SpriteBatch batch)
    {
      Texture2D texture = animation.SpriteSheet;
      Vector2 scale = new Vector2(camera.ScaleX, camera.ScaleY);

      batch.Draw(
      texture: texture,
      position: GetPosition(spacial, scale, animation),
      sourceRectangle: GetSourceRectangle(animation),
      scale: scale,
      color: Color.FromNonPremultiplied(255, 255, 255, (int)(animation.Opacity * 255)),
      effects: SpriteEffects.None,
      origin: Vector2.Zero,
      rotation: 0,
      layerDepth: 0
      );
    }

    private Rectangle? GetSourceRectangle(CAnimation animation)
    {
      return new Rectangle(
        animation.Index * 32 + animation.X * 32,
        32 * animation.Y + (int)animation.dir * 32,
        animation.Width, animation.Height);
    }

    private Vector2 GetPosition(CSpacial spacial, Vector2 scale, CAnimation animation)
    {
      Vector2 position = spacial.Position - new Vector2(animation.Width / 2, animation.Height / 2);
      position = camera.AbsoluteToRelative(position);
      return position * scale;
    }
  }

}
