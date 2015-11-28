using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Players
{
    public abstract class PlayerBase : BindableBase
    {
        public abstract string PlayerName { get; }

        public int NColors { get { return nColors; } set { nColors = value; } }
        protected int nColors = 2;
        public int ProgressionLength { get { return progressionLength; } set { progressionLength = value; } }
        protected int progressionLength = 3;
    }
}
