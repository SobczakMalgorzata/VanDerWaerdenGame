using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class RandomColorPlayer : ColorPlayerBase
    {
        private static Random rand = new Random();

        public override int GetColor(BoardState board)
        {
            return rand.Next(0, NColors);
        }

    }
}
