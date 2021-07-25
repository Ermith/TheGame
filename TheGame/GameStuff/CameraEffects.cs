using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.MyMath;

namespace TheGame.GameStuff
{
  partial class Camera
  {
    private float intensity = 0;
    private float zoom = 0;
    private float zoomingTime = 400f;
    private float zoomTime = 0f;
    private bool zoomingOut = false;
    private Random rnd = new Random();
    public Func<float, float> shadowTween = Tweens.SmoothStep4;
    private Func<float, float> zoomTween = Tweens.SmoothStep4;
    public void ShakeEffect(float intensity) => this.intensity = intensity;
    public void ZoomEffect(float zoom, float time)
    {
      if (!zoomingOut)
        return;

      zoomingTime = time;
      zoomTween = Tweens.SmoothStep4;
      this.zoom = zoom;
      zoomingOut = false;
      zoomTime = zoomingTime - zoomTime;
    }
    public void ZoomStop()
    {
      if (zoomingOut)
        return;

      zoomingOut = true;
      zoomTween = (float t) => Tweens.SmoothStep4(Tweens.Reverse(t));
      zoomTime = zoomingTime - zoomTime;
    }
    private void Shake()
    {
      float shakeX = (float)rnd.NextDouble() * intensity - intensity / 2;
      float shakeY = (float)rnd.NextDouble() * intensity - intensity / 2;

      OffsetX += shakeX;
      OffsetY += shakeY;

      intensity *= 0.9f;
      if (intensity < 0.3)
        intensity = 0;
    }
    private void Zoom(GameTime time)
    {
      zoomTime = MathF.Max(zoomTime - (float)time.ElapsedGameTime.TotalMilliseconds, 0);
      float t = 1 - zoomTime / zoomingTime;
      t = zoomTween(MathF.Min(t, 1));
      t = 1 + zoom * t;

      Width = (int)(defaultWidth * t);
      Height = (int)(defaultHeight * t);

      if (Width == defaultWidth)
        zoom = 0;
    }
  }
}
