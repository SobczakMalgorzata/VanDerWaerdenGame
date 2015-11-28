using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public abstract class ColorPlayerBase : BindableBase, IColorPlayer
    {
        protected int nColors = 2;
        public int NColors { get { return nColors; } set { nColors = value; } }

        public abstract int GetColor(BoardState board);
        
    }
}
