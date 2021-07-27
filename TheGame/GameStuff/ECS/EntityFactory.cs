﻿using Microsoft.Xna.Framework;
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
        builder
          .Spacial(position)
          .Movement(0.075f)
          .Input()
          .Animation(Assets.RogueAnimataion)
          .Behavior()
          .Light()
          .Health()
          .Attack();
      }

      if (name == "dummy")
      {
        ent = new Entity();
        builder.Target = ent;
        builder.Spacial(position).Movement().Animation(Assets.KnightAnimation).Behavior().Light().Health();
      }

      builder.Target = null;
      return ent;
    }
  }
}
