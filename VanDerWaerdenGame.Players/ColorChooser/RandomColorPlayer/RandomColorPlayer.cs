using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class RandomColorPlayer : IColorPlayer
    {
        private static Random rand = new Random();

        private int nColors = 2;
        public int NColors { get { return nColors; } set { nColors = value; } }

        public int GetPosition(BoardState board)
        {
            return rand.Next(0, nColors);
        }

    }
}
