using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.GameStuff
{
  class Animation
  {
    private float delta;
    private int defaultFrame;

    public Texture2D Sprite;
    public float Frequency { get; set; }
    public int CurrentFrameIndex { get; set; }
    public int width;
    public int heigth;
    public int frameCount;
    public bool Active { get; private set; }
    public Animation(Texture2D sprite, float frequency, int frameCount, int wid, int hei, int defaultFrame = 0)
    {
      Sprite = sprite;
      Frequency = frequency;
      this.frameCount = frameCount;
      width = wid;
      heigth = hei;
      Active = false;
      this.defaultFrame = defaultFrame;
      CurrentFrameIndex = defaultFrame;
    }

    public void Update(UpdateArguments arguments)
    {
      if (!Active) return;

      delta += (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;

      if (delta >= Frequency)
      {
        CurrentFrameIndex = (CurrentFrameIndex + 1) % frameCount;
        delta -= Frequency;
      }
    }

    public void Render(RenderArguments arguments, Vector2 pos, Color color, int column)
    {
      arguments.SpriteBatch.Draw(Sprite, pos, new Rectangle(CurrentFrameIndex * width, heigth * column, width, heigth), color);
    }

    public void Stop()
    {
      Active = false;
      CurrentFrameIndex = defaultFrame;
      delta = 0;
    }

    public void Restart()
    {
      Active = true;
      delta = 0;
      CurrentFrameIndex = defaultFrame;
    }
  }
}
