using System.Collections.Generic;
using TheGame.GameStuff.Entities;

namespace TheGame.GameStuff
{
  class EntityManager : IGameComponent
  {
    public Player Player;
    private List<Entity> entities;

    public EntityManager()
    {
      entities = new List<Entity>();
    }

    public void Add(Entity entity) => entities.Add(entity);

    public void Render(RenderArguments arguments)
    {
      Player.Render(arguments);

      foreach (Entity entity in entities)
        entity.Render(arguments);
    }

    public void Update(UpdateArguments arguments)
    {
      Player.Update(arguments);
      Player.Action?.Execute();

      foreach (Entity entity in entities)
      {
        entity.Update(arguments);
        entity.Action?.Execute();
      }
    }
  }
}
