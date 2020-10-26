using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TheGame.UI
{
  class UIControlManager : IGameComponent
  {
    List<UIControl> Controls;

    public UIControlManager()
    {
      Controls = new List<UIControl>();
    }

    public void AddControl(UIControl control) => Controls.Add(control);

    public void Render(SpriteBatch batch)
    {
      foreach (var control in Controls)
        control.Render(batch);
    }

    public void Update(GameTime time)
    {
      foreach (var control in Controls)
        control.Update(time);
    }
  }
}
