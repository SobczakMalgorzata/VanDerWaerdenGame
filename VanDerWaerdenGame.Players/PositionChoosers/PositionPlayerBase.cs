using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.PositionChoosers
{
    public abstract class PositionPlayerBase : PlayerBase, IPositionPlayer
    {
        public PositionPlayerBase(VanDerWaerdenGameRules rules) : base(rules) { }
        public abstract int GetPosition(int[] board);
    }
}
