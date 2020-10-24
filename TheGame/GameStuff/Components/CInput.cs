using System.Collections.Generic;
using TheGame.GameStuff.Entities;
using Microsoft.Xna.Framework.Input;

namespace TheGame.GameStuff.Components
{
  class CInput : Component
  {
    public static List<Entity> entities = new List<Entity>();
    public static void AddInputComponent(Entity entity)
    {
      entities.Add(entity);
      entity.Components[Components.Input] = new CInput();
    }

    // Controls
    public Keys Up = Keys.W;
    public Keys Left = Keys.A;
    public Keys Down = Keys.S;
    public Keys Right = Keys.D;
    public Keys Attack = Keys.Space;
  }
}
