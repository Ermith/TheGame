using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.MyMath;

namespace TheGame.GameStuff
{
  static class CameraOverlays
  {
    public static CAnimation Shadow(float frequency)
    {
      var shadow = new CAnimation();
      shadow.endingEffect = AnimationEffects.FadeOut;
      shadow.startingEffect = AnimationEffects.FadeIn;
      shadow.StartupTime = 100f;
      shadow.EndTime = 100f;
      shadow.Frequency = frequency;
      shadow.source = AnimationSource.Frames;
      shadow.Frames = Assets.ShadowOverlay;
      shadow.Width = GameEnvironment.ScreenWidth;
      shadow.Height = GameEnvironment.ScreenHeight;
      shadow.FrameCount = shadow.Frames.Length;
      shadow.loop = true;

      return shadow;
    }
  }

  class Camera
  {
    private static Random rnd = new Random();
    private static float intensity = 0;
    private static float zoom = 0;
    private static float zoomingTime = 400f;
    private static float zoomTime = 0f;
    private static bool zoomingOut = false;
    private static Func<float, float> zoomTween = Tweens.SmoothStep4;
    private static float offsetX;
    private static float offsetY;
    private static int defaultWidth;
    private static int defaultHeight;
    private static int MapWidth;
    private static int MapHeight;
    private static CAnimation shadowOverlay;
    public static Func<float, float> shadowTween = Tweens.SmoothStep4;
    private static float shadowFadeTime = 400f;
    private static float shadowTime = 0f;

    public static float OffsetX
    {
      get { return offsetX; }
      set { offsetX = System.Math.Clamp(value, 0, MapWidth - Width - 1); }
    }
    public static float OffsetY
    {
      get { return offsetY; }
      set { offsetY = System.Math.Clamp(value, 0, MapHeight - Height - 1); }
    }

    public static float X => OffsetX + Width / 2;
    public static float Y => OffsetY + Height / 2;

    public static int Width { get; set; }
    public static int Height { get; set; }

    public static float ScaleX => GameEnvironment.ScreenWidth / (float)Width;
    public static float ScaleY => GameEnvironment.ScreenHeight / (float)Height;

    public static List<CAnimation> Overlays = new List<CAnimation>();

    public static void Init(int defaultWidth, int defaultHeight, int mapWidth, int mapHeight)
    {
      Camera.defaultWidth = defaultWidth;
      Camera.defaultHeight = defaultHeight;
      MapWidth = mapWidth;
      MapHeight = mapHeight;
      ResetDimensions();
    }

    public static Vector2 AbsoluteToRelative(Vector2 absolute)
    {
      AbsoluteToRelative(absolute.X, absolute.Y, out float ox, out float oy);
      return new Vector2(ox, oy);
    }
    public static void AbsoluteToRelative(float x, float y, out float ox, out float oy)
    {
      ox = x - OffsetX;
      oy = y - OffsetY;
    }
    public static void Center(Vector2 position)
    {
      Center(position.X, position.Y);
    }
    public static void Center(float x, float y)
    {
      OffsetX = x - Width / 2;
      OffsetY = y - Height / 2;
    }

    public static void ResetDimensions()
    {
      Width = defaultWidth;
      Height = defaultHeight;
    }

    public static void Update(GameTime time)
    {
      if (intensity > 0)
        Shake();

      ResetDimensions();
      Zoom(time);
      Shadow(time);
    }

    private static void Shadow(GameTime time)
    {
      if (shadowOverlay == null)
        return;
      if (shadowOverlay.finished)
        return;

      shadowTime = MathF.Max(shadowTime - (float)time.ElapsedGameTime.TotalMilliseconds, 0);
      float t = 1 - shadowTime / shadowFadeTime;
      t = shadowTween(MathF.Min(t, 1));

      shadowOverlay.Opacity = t;
      if (t == 0)
        shadowOverlay.finished = true;
    }

    public static void ShakeEffect(float intensity) => Camera.intensity = intensity;
    public static void ZoomEffect(float zoom)
    {
      if (!zoomingOut)
        return;

      zoomTween = Tweens.SmoothStep4;
      Camera.zoom = zoom;
      zoomingOut = false;
      zoomTime = zoomingTime - zoomTime;
    }

    public static void ZoomStop()
    {
      if (zoomingOut)
        return;

      zoomingOut = true;
      zoomTween = (float t) => Tweens.SmoothStep4(Tweens.Reverse(t)); 
      zoomTime = zoomingTime - zoomTime;
    }
    public static void ShadowOverlayStart()
    {
      if (shadowOverlay == null)
      {
        shadowOverlay = CameraOverlays.Shadow(50f);
        Overlays.Add(shadowOverlay);
      }

      shadowOverlay.finished = false;
      shadowTween = Tweens.SmoothStep4;
      shadowTime = shadowFadeTime - shadowTime;
    }

    public static void ShadowOverlayStop()
    {
      if (shadowOverlay == null)
        return;

      shadowTween = (float t) => Tweens.SmoothStep4(Tweens.Reverse(t));
      shadowTime = shadowFadeTime - shadowTime;
    }

    private static void Shake()
    {
      float shakeX = (float)rnd.NextDouble() * intensity - intensity / 2;
      float shakeY = (float)rnd.NextDouble() * intensity - intensity / 2;

      OffsetX += shakeX;
      OffsetY += shakeY;

      intensity *= 0.9f;
      if (intensity < 0.3)
        intensity = 0;
    }
    private static void Zoom(GameTime time)
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
