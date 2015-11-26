using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Model
{
    public interface IGameRules
    {
        bool IsFinalStateOfGame(int[] board);
    }
}
