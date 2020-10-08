using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TheGame.GameStuff.Entities {
  class Player : Entity {
    public override void Render(RenderArguments arguments) {
      Point pos = Camera.AbsoluteToRelative(Position);
      arguments.SpriteBatch.Draw(Assets.placeHolder, pos.ToVector2(), Color.White);
    }

    public override void Update(UpdateArguments arguments) {
    }
  }
}
