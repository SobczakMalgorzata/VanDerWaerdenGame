using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class NeuralColorPlayer : ColorPlayerBase
    {
        public override string PlayerName { get { return "Neural network Player"; } }
        public override int GetColor(BoardState board)
        {
            throw new NotImplementedException();
        }
    }
}
