using Microsoft.Xna.Framework;
using System;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff.Actions {
  class Move : Action {
    private Entity entity;
    private Vector2 movement;

    public Move(Entity entity, Vector2 movement) {
      this.entity = entity;
      this.movement = movement;
    }
    public override void Execute() {
      entity.Position += movement;
    }
  }
}
