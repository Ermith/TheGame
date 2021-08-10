using CaveGenerator;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace MapEditor
{
  class MapWindow : Window
  {
    TileType[,] Map;
    int MapWidth => Map.GetLength(0);
    int MapHeight => Map.GetLength(1);
    TileTextureMapper TextureMapper;
    Camera Camera;
    MouseState state;
    private int tileWid = 32;
    private int tileHei = 32;
    private Vector2 TileSize => new Vector2(tileWid, tileHei);

    public MapWindow(
      int x, int y,
      int width, int height,
      GraphicsDevice graphics,
      TileType[,] map,
      TileTextureMapper textureMapper,
      Camera camera)
      : base(x, y, width, height, graphics)
    {
      Map = map;
      TextureMapper = textureMapper;
      Camera = camera;
    }

    public override void Update(float time)
    {
      Camera.Update(time);
      state = Mouse.GetState();

      TileType t = null;
      if (state.LeftButton == ButtonState.Pressed) t = Info.Tile;
      if (state.RightButton == ButtonState.Pressed) t = new RockTile();

      if (t != null)
      {
        var scale = new Vector2(
          Width / (float)Camera.Width,
          Height / (float)Camera.Height
        );

        Vector2 position = state.Position.ToVector2();

        position -= Position;
        if (position.X < 0 || position.X >= Width || position.Y < 0 && position.Y >= Height)
          return;

        position /= scale;
        position += Camera.Offset;
        position.X /= tileWid;
        position.Y /= tileHei;


        if (position.X >= 0 && position.X < MapWidth && position.Y >= 0 && position.Y < MapWidth)
          Map[(int)position.X, (int)position.Y] = t;
      }
    }

    private Color GetColor(Vector2 pos, Vector2 step)
    {
      var m = state.Position.ToVector2() - Position;
      return (
        m.X >= pos.X &&
        m.Y >= pos.Y &&
        m.X < pos.X + step.X &&
        m.Y < pos.Y + step.Y)
        ? Color.Red
        : Color.White;
    }

    protected override void RenderSimple(SpriteBatch batch)
    {
      int mapWid = Map.GetLength(0);
      int mapHei = Map.GetLength(1);
      var scale = new Vector2(Width / (float)Camera.Width, Height / (float)Camera.Height);
      var startTile = Camera.Offset / TileSize;
      Vector2 start = (startTile * TileSize - Camera.Offset) * scale;
      Vector2 endTile = (Camera.Offset + Camera.Size) / TileSize;
      Vector2 step = TileSize * scale;
      Vector2 t;

      for (t.X = (int)startTile.X; t.X < endTile.X; t.X++)
        for (t.Y = (int)startTile.Y; t.Y < endTile.Y; t.Y++)
          if (t.X >= 0 && t.X < mapWid && t.Y >= 0 && t.Y < mapHei)
          {
            Vector2 pos = start + (t - startTile) * step;

            batch.Draw(
              TextureMapper.Get(Map[(int)t.X, (int)t.Y]),
              pos,
              null,
              GetColor(pos, step),
              0,
              Vector2.Zero,
              scale,
              SpriteEffects.None,
              0
              );
          }
    }
  }
}
