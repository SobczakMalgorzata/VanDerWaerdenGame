using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.PositionChoosers
{
    public class RandomPositionPlayer : PositionPlayerBase
    {
        public override string PlayerName { get { return "Random Player"; } }
        public RandomPositionPlayer(VanDerWaerdenGameRules rules) : base(rules) { }

        public override int GetPosition(int[] board)
        {
            return rand.Next(0, board.Count()+1);
        }

        private static Random rand = new Random();

    }
}
