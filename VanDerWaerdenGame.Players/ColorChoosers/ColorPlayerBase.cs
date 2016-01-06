using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public abstract class ColorPlayerBase : PlayerBase, IColorPlayer
    {
        public ColorPlayerBase(VanDerWaerdenGameRules rules) : base(rules) { }
        public abstract int GetColor(BoardState board);
    }
}
