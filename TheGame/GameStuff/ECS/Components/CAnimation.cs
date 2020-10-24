using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS;

namespace TheGame.GameStuff.ECS.Components
{
  class CAnimation : Component
  {
    public static List<Entity> entities = new List<Entity>();
    public Texture2D Sprite { get; set; }
    public bool Active { get; private set; }
    public float Delta { get; set; }
    public float Frequency { get; set; }
    public int DefaultFrame { get; set; }
    public int CurrentFrameIndex { get; set; }
    public int Width { get; set; }
    public int Heigth { get; set; }
    public int FrameCount { get; set; }
    
    public static void AddAnimationComponent(Entity entity, Texture2D sprite, float frequency, int frameCount, int wid, int hei, int defaultFrame = 0)
    {
      entities.Add(entity);
      entity.Components[ComponentTypes.Animation] = new CAnimation(sprite, frequency, frameCount, wid, hei, defaultFrame);
    }

    public CAnimation(Texture2D sprite, float frequency, int frameCount, int wid, int hei, int defaultFrame = 0)
    {
      Sprite = sprite;
      Frequency = frequency;
      FrameCount = frameCount;
      Width = wid;
      Heigth = hei;
      Active = false;
      DefaultFrame = defaultFrame;
      CurrentFrameIndex = defaultFrame;
    }

  }
}
