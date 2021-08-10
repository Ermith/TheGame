using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapEditor
{
  abstract class Window
  {
    public float X;
    public float Y;
    public int Width;
    public int Height;
    protected RenderTarget2D RenderTarget;
    protected GraphicsDevice Graphics;

    abstract public void Update(float time);
    abstract protected void RenderSimple(SpriteBatch batch);

    public Rectangle Bounds => new Rectangle((int)X, (int)Y, Width, Height);
    public Vector2 Position
    {
      get
      {
        return new Vector2(X, Y);
      }
      set
      {
        X = value.X;
        Y = value.Y;
      }
    }

    public Window(int x, int y, int width, int height, GraphicsDevice graphics)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
      RenderTarget = new RenderTarget2D(graphics, width, height);
      Graphics = graphics;
    }
    public (float, float) ToRelative(float x, float y)
    {
      x -= X;
      y -= Y;

      return (x, y);
    }
    public Vector2 ToRelative(Vector2 pos)
    {
      (float x, float y) = ToRelative(pos.X, pos.Y);
      return new Vector2(x, y);
    }

    public void Render(SpriteBatch batch)
    {
      Graphics.SetRenderTarget(RenderTarget);
      batch.Begin();
      RenderSimple(batch);
      batch.End();
    }

    public void RenderMain(SpriteBatch batch)
    {
      batch.Draw(RenderTarget, new Vector2(X, Y), Color.White);
    }
  }
}
