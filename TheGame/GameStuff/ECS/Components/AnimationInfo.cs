using System.Collections.Generic;

namespace TheGame.GameStuff.ECS.Components
{
  struct AnimationInfo
  {
    Dictionary<State, (int, int)> FrameCoords;
    Dictionary<State, int> FrameCounts;
  }
}
