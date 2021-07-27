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
    public LightSystem lightSystem;
    public HealthSystem healthSystem;
    public AttackSystem attackSystem;

    public World World { get; }

    public SystemManager(World world, Camera camera)
    {
      World = world;

      // init Entities
      tracker = new ComponentTracker();
      factory = new EntityFactory(new ComponentBuilder(tracker));
      var pos = world.GenerateSpawnPoint();
      Player = factory.Build("player", pos);
      factory.Build("dummy", pos + new Vector2(40, 40));

      // init systems
      inputSystem = new InputSystem(tracker.GetEntities<CInput>(), camera);
      collisionSystem = new CollisionSystem(World, tracker.GetEntities<CMovement>());
      animationSystem = new AnimationSystem(tracker.GetEntities<CAnimation>());
      renderSystem = new RenderSystem(tracker.GetEntities<CAnimation>(), tracker.GetEntities<CLight>(), camera);
      lightSystem = new LightSystem(tracker.GetEntities<CLight>());
      healthSystem = new HealthSystem(tracker);
      attackSystem = new AttackSystem(tracker.GetEntities<CAttack>(), tracker.GetEntities<CHealth>());
    }

    public void Render(SpriteBatch batch)
    {
      renderSystem.Render(batch);
    }

    public void RenderLight(SpriteBatch batch)
    {
      renderSystem.RenderLight(batch);
    }

    public void Update(GameTime time)
    {
      inputSystem.Update(time);
      collisionSystem.Update(time);
      animationSystem.Update(time);
      attackSystem.Update(time);
      healthSystem.Update(time);
      lightSystem.Update(time);
    }
  }
}
