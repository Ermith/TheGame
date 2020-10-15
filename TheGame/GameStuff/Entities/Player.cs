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
    private enum direction { up = 0, left = 1, down = 2, right = 3 };
    private direction dir = direction.up;
    private Animation animation;

    public Player(World world) : base(world)
    {
      Width = 32;
      Height = 32;
      animation = new Animation(Assets.PlayerSprite, 85, 4, 32, 32, 1);
    }

    public override void Render(RenderArguments arguments)
    {
      Camera.Center(
        (int)Position.X + 16,
        (int)Position.Y + 16
        );
      Point pos = Camera.AbsoluteToRelative(Position.ToPoint());

      animation.Render(arguments, pos.ToVector2(), Color.White, (int)dir);
    }

    public override void Update(UpdateArguments arguments)
    {
      if (arguments.Keyboard.GetPressedKeys().Count() == 0)
      {
        Action = null;
        animation.Stop();
        return;
      }

      Velocity = new Vector2(0, 0);

      if (arguments.Keyboard.IsKeyDown(Keys.W))
      {
        Velocity += CommonVectors.Up;
        dir = direction.up;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.S))
      {
        Velocity += CommonVectors.Down;
        dir = direction.down;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.A))
      {
        Velocity += CommonVectors.Left;
        dir = direction.left;
      }
      if (arguments.Keyboard.IsKeyDown(Keys.D))
      {
        Velocity += CommonVectors.Right;
        dir = direction.right;
      }

      if (Velocity.Length() != 0)
      {
        Velocity.Normalize();
        if (!animation.Active)
          animation.Restart();
      }
      else
        animation.Stop();

      animation.Update(arguments);

      Movement = Velocity * speed * (float)arguments.Time.ElapsedGameTime.TotalSeconds;

      Action = new Move(this, Movement);
    }
  }
}
