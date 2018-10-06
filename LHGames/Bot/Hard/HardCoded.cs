using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Helper;

public static class HardCoded
{
   public static int currentHardCodedValue;
   public static int[,] attackValue = new int[,] { { 0, -1 } };
   public static int[,] roadToVictory = new int[,] { { 0, -1 },
       { 0, 1 },
       { 1, 0 },
       { 1, 0 },
       { 1, 0 },
       { 1, 0 },
       { 1, 0 },
   };
}
