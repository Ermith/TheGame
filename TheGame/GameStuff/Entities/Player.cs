using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheGame.GameStuff.Actions;
using TheGame.Math;

namespace TheGame.GameStuff.Entities
{
  class Player : Entity
  {
    Vector2 Velocity = new Vector2(1.5f, 1.5f);
    float speed = 5f;

    public override void Render(RenderArguments arguments)
    {
      Camera.Center(
        (int)Position.X + 16,
        (int)Position.Y + 16
        );
      Point pos = Camera.AbsoluteToRelative(Position.ToPoint());
      arguments.SpriteBatch.Draw(Assets.placeHolder, pos.ToVector2(), Color.White);
    }

    public override void Update(UpdateArguments arguments)
    {
      if (arguments.Keyboard.GetPressedKeys().Count() == 0)
      {
        Action = null;
        return;
      }

      Velocity = new Vector2(0, 0);

      if (arguments.Keyboard.IsKeyDown(Keys.W))
        Velocity += CommonVectors.Up;
      if (arguments.Keyboard.IsKeyDown(Keys.A))
        Velocity += CommonVectors.Left;
      if (arguments.Keyboard.IsKeyDown(Keys.S))
        Velocity += CommonVectors.Down;
      if (arguments.Keyboard.IsKeyDown(Keys.D))
        Velocity += CommonVectors.Right;

      Action = new Move(this, Velocity * speed);
    }
  }
}
