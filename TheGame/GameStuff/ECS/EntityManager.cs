using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using TheGame.GameStuff.ECS.Systems;

namespace TheGame.GameStuff.ECS
{
  class EntityManager : IGameComponent
  {
    public Entity Player;
    private List<Entity> entities;
    public InputSystem inputSystem;
    public CollisionSystem collisionSystem;
    public RenderSystem renderSystem;

    public World World { get; }

    public EntityManager(World world)
    {
      entities = new List<Entity>();
      World = world;
      inputSystem = new InputSystem();
      collisionSystem = new CollisionSystem(World);
      renderSystem = new RenderSystem();

      Player = new Entity();
      CInput.AddInputComponent(Player);
      CSpacial.AddLocationComponent(Player);
      CMovement.AddMovementComponent(Player);
      CAnimation.AddAnimationComponent(Player, Assets.PlayerSprite, 85, 4, 32, 32, 1);
    }

    public void Add(Entity entity) => entities.Add(entity);

    public void Render(RenderArguments arguments)
    {
      renderSystem.Render(arguments);
    }

    public void Update(UpdateArguments arguments)
    {
      inputSystem.Update(arguments);
      collisionSystem.Update(arguments);
      renderSystem.Update(arguments);
    }
  }
}
