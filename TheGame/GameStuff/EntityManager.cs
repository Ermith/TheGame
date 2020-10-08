using System.Collections.Generic;

namespace TheGame.GameStuff {
  class EntityManager : IGameComponent {
    private List<Entity> entities;

    public EntityManager() {
      entities = new List<Entity>();
    }

    public void Add(Entity entity) => entities.Add(entity);

    public void Render(RenderArguments arguments) {
      foreach (Entity entity in entities)
        entity.Render(arguments);
    }

    public void Update(UpdateArguments arguments) {
      foreach (Entity entity in entities)
        entity.Update(arguments);
    }
  }
}
