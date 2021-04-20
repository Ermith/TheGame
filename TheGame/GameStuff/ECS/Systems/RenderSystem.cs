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

      foreach (CAnimation animation in Camera.Overlays)
      {
        //animation.Width = Camera.Width;
        //animation.Height = Camera.Height;
        RenderAnimation(animation, new CSpacial() { X = Camera.OffsetX + animation.Width / 2, Y = Camera.OffsetY + animation.Height / 2 }, batch);
      }
    }

    private void RenderAnimation(CAnimation animation, CSpacial spacial, SpriteBatch batch)
    {
      Texture2D texture = GetTexture(animation);
      Vector2 scale = GetScale(texture, animation);

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

    private Rectangle? GetSourceRectangle(CAnimation animation) => animation.source switch
    {
      AnimationSource.Sprite => new Rectangle(
        animation.Index * 32 + animation.X * 32,
        32 * animation.Y + (int)animation.dir * 32,
        animation.Width, animation.Height),
      _ => null
    };

    private Texture2D GetTexture(CAnimation animation) => animation.source switch
    {
      AnimationSource.Frames => animation.Frames[animation.Index],
      AnimationSource.Sprite => animation.Sprite,
      _ => null
    };

    private Vector2 GetPosition(CSpacial spacial, Vector2 scale, CAnimation animation)
    {
      Vector2 position = spacial.Position - new Vector2(animation.Width / 2, animation.Height / 2);
      position = Camera.AbsoluteToRelative(position);
      return position * scale;
    }

    private Vector2 GetScale(Texture2D texture, CAnimation animation)
    {
      if (animation.source == AnimationSource.Sprite)
        return new Vector2(Camera.ScaleX, Camera.ScaleY);

      var scale = new Vector2();
      scale.X = (float)animation.Width / texture.Width;// * Camera.ScaleX;
      scale.Y = (float)animation.Height / texture.Height;// * Camera.ScaleY;

      return scale;
    }
  }

}
