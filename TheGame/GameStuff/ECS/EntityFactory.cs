using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using TheGame.GameStuff.ECS.Components;

namespace TheGame.GameStuff.ECS
{
  class EntityFactory
  {
    private readonly ComponentBuilder builder;

    public EntityFactory(ComponentBuilder builder)
    {
      this.builder = builder;
    }

    public Entity Build(string name, Vector2 position)
    {
      Entity ent = null;

      if (name == "player")
      {
        ent = new Entity();
        builder.Target = ent;
        builder.Spacial(position).Movement().Input().Animation(Assets.PlayerSprite, 4, 32, 32, defaultFrame: 1);
      }

      builder.Target = null;
      return ent;
    }
  }
}
