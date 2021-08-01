using System.IO;
using System.Collections.Generic;
using TheGame.GameStuff.ECS.Components;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.GameStuff.ECS
{
  static class AnimationParser
  {
    private const string FrameCountToken = "FrameCount";
    private const string XToken = "X";
    private const string YToken = "Y";
    private const string StateToken = "State";
    private const char SplitToken = '=';
    private static readonly char[] WhiteSpaces = {'\n', '\r', '\t', ' '};
    private static readonly HashSet<State> AttackStates = new HashSet<State>(new []{ State.AttackWindup, State.Attacking, State.AttackFinish });

    private static void ParseLine(Dictionary<string, string> dict, string line)
    {
      string[] parts = line.Split(WhiteSpaces, StringSplitOptions.RemoveEmptyEntries);
      foreach (string part in parts)
      {
        string[] KeyValue = part.Split(SplitToken);
        dict[KeyValue[0]] = KeyValue[1];
      }
    }

    private static void CheckForToken(Dictionary<string, string> dict, string token, string fileName)
    {
      if (!dict.ContainsKey(token))
        throw new InvalidDataException($"Missing token {token} in file {fileName}");
    }

    public static CAnimation ParseSettings(string infoFile, Texture2D spriteSheet)
    {
      var animation = new CAnimation();
      var reader = new StreamReader(infoFile);
      string line;
      var dict = new Dictionary<string, string>();
      animation.SpriteSheet = spriteSheet;

      while((line = reader.ReadLine()) != null)
      {
        if (line == string.Empty)
          continue;

        dict.Clear();
        ParseLine(dict, line);

        // Check for presence
        CheckForToken(dict, StateToken, infoFile);
        CheckForToken(dict, XToken, infoFile);
        CheckForToken(dict, YToken, infoFile);
        CheckForToken(dict, FrameCountToken, infoFile);

        // Check for correction
        if (!Enum.TryParse(dict[StateToken], out State state))
          throw new InvalidDataException($"Unknown State '{dict[StateToken]}' in file '{infoFile}'.");
        if (!uint.TryParse(dict[XToken], out uint x))
          throw new InvalidDataException($"Unknown X value '{dict[XToken]}' in file '{infoFile}'.");
        if (!uint.TryParse(dict[YToken], out uint y))
          throw new InvalidCastException($"Unknonw Y value '{dict[YToken]}' in file '{infoFile}'.");
        if (!uint.TryParse(dict[FrameCountToken], out uint frameCount))
          throw new InvalidCastException($"Unknonw FrameCount value '{dict[FrameCountToken]}' in file '{infoFile}'.");

        if (AttackStates.Contains(state))
        {
          if (state == State.AttackWindup) {
            animation.attackFrameCounts.Add(new Dictionary<State, int>());
            animation.attackFrameCoords.Add(new Dictionary<State, (int, int)>());
          }
          animation.attackFrameCounts[animation.attackFrameCounts.Count - 1][state] = ((int)frameCount);
          animation.attackFrameCoords[animation.attackFrameCoords.Count - 1][state] = ((int)x, (int)y);
        } else
        {
          animation.frameCounts[state] = (int)frameCount;
          animation.frameCoords[state] = ((int)x, (int)y);
        }
      }

      animation.FrameCount = 1;
      return animation;
    }
  }
}
