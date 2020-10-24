﻿using Microsoft.Xna.Framework;
using TheGame.GameStuff.Components;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff.Systems
{
  class CollisionSystem : System
  {
    public CollisionSystem(World world)
    {
      World = world;
    }

    public World World { get; }

    public override void Update(UpdateArguments arguments)
    {
      foreach (Entity entity in CMovement.entities)
      {
        CMovement movement = entity.Components[Component.Components.Movement] as CMovement;
        CLocation location = entity.Components[Component.Components.Location] as CLocation;

        float newX = location.X;
        float newY = location.Y;
        

        // Vertical movement
        if (World.CheckPosition(
          new Rectangle(
            (int)(location.X + movement.dX),
            (int)location.Y,
            32,
            32
            )
          ))
          newX += movement.dX;

        // Horizontal movement
        if (World.CheckPosition(
          new Rectangle(
            (int)location.X,
            (int)(location.Y + movement.dY),
            32,
            32
            )
          ))
          newY += movement.dY;

        location.X = (int)newX;
        location.Y = (int)newY;
      }
    }
  }
}
