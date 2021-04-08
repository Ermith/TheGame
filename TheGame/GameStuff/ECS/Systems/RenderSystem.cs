using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS.Systems
{
  class RenderSystem : IRenderable
  {
    private List<Entity> animationEntities;

    public RenderSystem(List<Entity> animationEntities)
    {
      this.animationEntities = animationEntities;
    }

    public void Render(SpriteBatch batch)
    {
      foreach (Entity entity in animationEntities)
        RenderAnimation(entity.Get<CAnimation>(), entity.Get<CSpacial>(), batch);
    }

    private void RenderAnimation(CAnimation animation, CSpacial spacial, SpriteBatch batch)
    {
      batch.Draw(
      texture: GetTexture(animation),
      position: GetPosition(spacial),
      sourceRectangle: GetSourceRectangle(animation),
      scale: new Vector2(Camera.ScaleX, Camera.ScaleY),
      color: Color.FromNonPremultiplied(255, 255, 255, (int)(animation.Opacity * 255)),
      effects: SpriteEffects.None,
      origin: Vector2.Zero,
      rotation: 0,
      layerDepth: 0
  );
    }

    private Rectangle? GetSourceRectangle(CAnimation animation) => animation.source switch
    {
      AnimationSource.Sprite => new Rectangle(
        animation.Index * 32,
        animation.Height * animation.Row,
        animation.Width, animation.Height),
      _ => null
    };

    private Texture2D GetTexture(CAnimation animation) => animation.source switch
    {
      AnimationSource.Frames => animation.Frames[animation.Index],
      AnimationSource.Sprite => animation.Sprite,
      _ => null
    };

    private Vector2 GetPosition(CSpacial spacial)
    {
      Vector2 position = spacial.HitBox.Location.ToVector2();
      position = Camera.AbsoluteToRelative(position);
      position.X *= Camera.ScaleX;
      position.Y *= Camera.ScaleY;

      return position;
    }
  }

}
