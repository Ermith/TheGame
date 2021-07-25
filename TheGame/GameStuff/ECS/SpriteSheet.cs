using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS
{
  struct SpriteSheet
  {
    public int Width;
    public int Hieght;
    public Texture2D Sheet;
    public Dictionary<State, (int, int, int)> StateData;
  }
}
