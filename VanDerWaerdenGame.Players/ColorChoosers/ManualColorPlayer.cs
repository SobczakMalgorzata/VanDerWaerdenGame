using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class ManualColorPlayer : ColorPlayerBase
    {
        public ManualColorPlayer(VanDerWaerdenGameRules rules) : base(rules) { }
        public override string PlayerName {  get { return "Manual Player"; } }
        public override int GetColor(BoardState board)
        {
            throw new NotImplementedException();
        }
    }
}
