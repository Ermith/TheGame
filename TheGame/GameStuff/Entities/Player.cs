using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
      Camera.Center(Position.ToPoint());
      Point pos = Camera.AbsoluteToRelative(Position.ToPoint());
      arguments.SpriteBatch.Draw(Assets.placeHolder, pos.ToVector2(), Color.White);
    }

    public override void Update(UpdateArguments arguments)
    {

      if (arguments.Keyboard.IsKeyDown(Keys.W))
        Action = new Move(this, CommonVectors.Up * speed);
      else if (arguments.Keyboard.IsKeyDown(Keys.A))
        Action = new Move(this, CommonVectors.Left * speed);
      else if (arguments.Keyboard.IsKeyDown(Keys.S))
        Action = new Move(this, CommonVectors.Down * speed);
      else if (arguments.Keyboard.IsKeyDown(Keys.D))
        Action = new Move(this, CommonVectors.Right * speed);
      else
        Action = null;
    }
  }
}
