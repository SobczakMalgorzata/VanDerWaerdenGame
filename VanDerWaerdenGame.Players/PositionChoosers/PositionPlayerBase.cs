using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.PositionChoosers
{
    public abstract class PositionPlayerBase : IPositionPlayer
    {
        public abstract int GetPosition(int[] board);
    }
}
