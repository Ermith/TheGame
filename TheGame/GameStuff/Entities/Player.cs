using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using TheGame.GameStuff.Actions;
using TheGame.Math;
using TheGame.States;

namespace TheGame.GameStuff.Entities
{
  class Player : Entity
  {
    


    Vector2 Velocity = new Vector2(1.5f, 1.5f);
    float speed = 300f;
    private Vector2 Movement;
    private Animation animUp, animLeft, animDown, animRight;
    private Animation currAnim;

    public Player(World world) : base(world)
    {
      Width = 32;
      Height = 32;
    }

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
      {
        Velocity += CommonVectors.Up;
        currAnim = animUp;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.A))
      {
        Velocity += CommonVectors.Left;
        currAnim = animLeft;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.S))
      {
        Velocity += CommonVectors.Down;
        currAnim = animDown;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.D))
      {
        Velocity += CommonVectors.Right;
        currAnim = animRight;
      }

      if (Velocity.Length() != 0)
        Velocity.Normalize();

      Movement = Velocity * speed * (float)arguments.Time.ElapsedGameTime.TotalSeconds;

      Action = new Move(this, Movement);
    }
  }
}
