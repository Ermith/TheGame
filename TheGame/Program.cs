﻿using System;

namespace TheGame
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var game = new TheGame())
        game.Run();
    }
  }
}
