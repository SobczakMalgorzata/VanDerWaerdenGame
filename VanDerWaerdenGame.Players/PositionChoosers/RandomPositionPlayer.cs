using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.PositionChoosers
{
    public class RandomPositionPlayer : IPositionPlayer
    {
        private static Random rand = new Random();
        public int GetPosition(int[] board)
        {
            return rand.Next(0, board.Count()+1);
        }
    }
}
