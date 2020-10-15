using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.GameStuff
{
  class Animation
  {
    private float start;
    private float last;
    private float delta;

    public List<Texture2D> Frames { get; set; }
    public float Frequency { get; set; }
    public int CurrentFrameIndex { get; set; }
    public Texture2D CurrentFrame => Frames[CurrentFrameIndex];
    public int FrameCount => Frames.Count;
    public Animation(List<Texture2D> frames, float frequency)
    {
      Frames = frames;
      Frequency = frequency;
    }

    public void Update(UpdateArguments arguments)
    {
      delta += (float)arguments.Time.ElapsedGameTime.TotalMilliseconds;

      if (delta >= Frequency)
      {
        CurrentFrameIndex = (CurrentFrameIndex + 1) % FrameCount;
        delta -= Frequency;
      }
    }

    public void Stop()
    {

    }

    public void Restart()
    {

    }
  }
}
