using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS;

namespace TheGame.GameStuff.ECS.Components
{
  class CAnimation : Component
  {
    public Texture2D Sprite { get; set; }
    public bool Active { get;  set; }
    public float Delta { get; set; }
    public float Frequency { get; set; }
    public int DefaultFrame { get; set; }
    public int CurrentFrameIndex { get; set; }
    public int Width { get; set; }
    public int Heigth { get; set; }
    public int FrameCount { get; set; }

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
