using System.Collections.Generic;

namespace TheGame.UI {
  class UIControlManager : IGameComponent {
    List<UIControl> Controls;

    public UIControlManager() {
      Controls = new List<UIControl>();
    }

    public void AddControl(UIControl control) => Controls.Add(control);

    public void Render(RenderArguments arguments) {
      foreach (var control in Controls)
        control.Render(arguments);
    }

    public void Update(UpdateArguments arguments) {
      foreach (var control in Controls)
        control.Update(arguments);
    }
  }
}
