using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff.Components
{
  class CRender : Component
  {
    public static List<Entity> entities = new List<Entity>();

    public static void AddRenderComponent(Entity entity, Texture2D sprite, float frequency, int frameCount, int wid, int hei, int defaultFrame = 0)
    {
      entities.Add(entity);
      entity.Components[Components.Render] = new CRender(sprite, frequency, frameCount, wid, hei, defaultFrame);
    }

    public CRender(Texture2D sprite, float frequency, int frameCount, int wid, int hei, int defaultFrame = 0)
    {
      Sprite = sprite;
      Frequency = frequency;
      FrameCount = frameCount;
      Width = wid;
      Heigth = hei;
      Active = false;
      this.defaultFrame = defaultFrame;
      CurrentFrameIndex = defaultFrame;
    }

    public float delta { get; set; }
    public int defaultFrame { get; set; }
    public Texture2D Sprite { get; set; }
    public float Frequency { get; set; }
    public int CurrentFrameIndex { get; set; }
    public int Width { get; set; }
    public int Heigth { get; set; }
    public bool Active { get; private set; }
    public int FrameCount { get; set; }
  }
}
