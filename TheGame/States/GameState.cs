using System;
using TheGame.GameStuff;
using TheGame.GameStuff.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame.States {
  class GameState : State {
    EntityManager entityManager;
    Player Player = new Player();

    // Prototype
    private Color[,] Map;
    private const int tileSize = 32;
    private int wid = 200;
    private int hei = 200;

    public GameState() {
      Map = new Color[200, 200];

      entityManager = new EntityManager();

      Random rnd = new Random(32);

      for (int i = 0; i < 200; i++) {
        for (int j = 0; j < 200; j++) {
          Map[i, j] = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }
      }
    }

    public override void Render(RenderArguments arguments) {
      Utilities.IsMouseVisible = false;

      // Background
      int xStart = Camera.Offset.X - (Camera.Offset.X % tileSize);
      int yStart = Camera.Offset.Y - (Camera.Offset.Y % tileSize);

      int wid = Camera.Width / tileSize + 1;
      int hei = Camera.Height / tileSize + 1;

      /**/
      for (int x = 0; x < wid; x++)
        for (int y = 0; y < hei; y++) {

          int xx = x * tileSize + xStart;
          int yy = y * tileSize + yStart;

          Camera.AbsoluteToRelative(xx, yy, out int ox, out int oy);

          Texture2D rect = new Texture2D(arguments.Graphics, tileSize, tileSize);
          Color[] data = new Color[tileSize * tileSize];
          Color col = Map[x + Camera.Offset.X / tileSize, y + Camera.Offset.Y / tileSize];

          for (int i = 0; i < tileSize * tileSize; i++) data[i] = col;
          rect.SetData(data);

          arguments.SpriteBatch.Draw(rect,
            new Vector2(ox, oy),
            Color.White
            );
        }
      /**/

      // Player

      // Entities
      entityManager.Render(arguments);
    }

    public override void Update(UpdateArguments arguments) {

      // Background

      // Entities
      entityManager.Update(arguments);
    }
  }
}
