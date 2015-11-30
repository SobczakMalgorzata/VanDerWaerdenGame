using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Players
{
    public class GradingFunctions
    {
        public static double ScoreColorPlayer (int[] scores)
        {
            return scores.Select(score => Math.Pow(0.35, 4 - score)).Average();
        }
    }
}
