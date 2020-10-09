using System.Collections.Generic;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff
{
  class EntityManager : IGameComponent
  {
    public Player player = new Player();
    private List<Entity> entities;

    public EntityManager()
    {
      entities = new List<Entity>();
    }

    public void Add(Entity entity) => entities.Add(entity);

    public void Render(RenderArguments arguments)
    {
      player.Render(arguments);

      foreach (Entity entity in entities)
        entity.Render(arguments);
    }

    public void Update(UpdateArguments arguments)
    {
      player.Update(arguments);
      player.Action?.Execute();

      foreach (Entity entity in entities)
      {
        entity.Update(arguments);
        entity.Action?.Execute();
      }
    }
  }
}
