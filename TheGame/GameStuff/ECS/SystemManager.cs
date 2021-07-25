using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.GameStuff.ECS.Systems;
using TheGame.Math;

namespace TheGame.GameStuff.ECS
{
  class SystemManager : IGameComponent
  {
    public Entity Player;
    private readonly ComponentTracker tracker;
    private readonly EntityFactory factory;
    public InputSystem inputSystem;
    public CollisionSystem collisionSystem;
    public RenderSystem renderSystem;
    public AnimationSystem animationSystem;
    private Camera camera;

    public World World { get; }

    public SystemManager(World world, Camera camera)
    {
      World = world;
      this.camera = camera;

      // init Entities
      tracker = new ComponentTracker();
      factory = new EntityFactory(new ComponentBuilder(tracker));
      Player = factory.Build("player", world.GenerateSpawnPoint());

      // init systems
      inputSystem = new InputSystem(tracker.GetEntities<CInput>(), camera);
      collisionSystem = new CollisionSystem(World, tracker.GetEntities<CMovement>());
      animationSystem = new AnimationSystem(tracker.GetEntities<CAnimation>());
      renderSystem = new RenderSystem(tracker.GetEntities<CAnimation>(), camera);
    }

    public void Render(SpriteBatch batch)
    {
      renderSystem.Render(batch);
    }

    public void Update(GameTime time)
    {
      inputSystem.Update(time);
      collisionSystem.Update(time);
      animationSystem.Update(time);
    }
  }
}
