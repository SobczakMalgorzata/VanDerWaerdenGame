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
        public override string PlayerName { get { return "Random Player"; } }

        public override int GetColor(BoardState board)
        {
            return rand.Next(0, this.NColors);
        }

        private static Random rand = new Random();
    }
}
