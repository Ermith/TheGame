using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheGame.Math;
using Microsoft.Xna.Framework.Graphics;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS
{
  class ComponentBuilder
  {
    public Entity Target { get; set; }
    private ComponentTracker tracker;

    public ComponentBuilder(ComponentTracker tracker, Entity target = null)
    {
      this.tracker = tracker;
      Target = target;
    }

    public ComponentBuilder Spacial(Vector2 position, Direction facing = Direction.Up, int wid = 32, int hei = 32)
    {
      CSpacial spacial = new CSpacial();
      spacial.Position = position;
      spacial.Facing = facing;
      spacial.Width = wid;
      spacial.Height = hei;

      Target.Add(spacial);
      tracker.Add(Target, spacial);

      return this;
    }

    public ComponentBuilder Movement(float speed = 0.2f)
    {
      CMovement movement = new CMovement();
      movement.Speed = speed;
      movement.Velocity = Vector2.Zero;

      Target.Add(movement);
      tracker.Add(Target, movement);
      return this;
    }

    public ComponentBuilder Input(
      Keys up = Keys.W,
      Keys left = Keys.A,
      Keys down = Keys.S,
      Keys right = Keys.D,
      Keys attack = Keys.Space)
    {
      CInput input = new CInput();
      input.Up = up;
      input.Left = left;
      input.Down = down;
      input.Right = right;
      input.Attack = attack;

      Target.Add(input);
      tracker.Add(Target, input);
      return this;
    }

    public ComponentBuilder Animation(
      Texture2D sprite,
      int framecount,
      int wid,
      int hei,
      float frequency = 100,
      bool active = false,
      int defaultFrame = 0,
      float delta = 0
      )
    {
      CAnimation anim = new CAnimation(sprite, frequency, framecount, wid, hei);
      anim.Active = active;
      anim.DefaultFrame = defaultFrame;
      anim.Delta = delta;

      Target.Add(anim);
      tracker.Add(Target, anim);
      return this;
    }
  }
}
