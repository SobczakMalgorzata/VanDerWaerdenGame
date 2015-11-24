using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.DesktopApp
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel() : base()
        {
            Board = new int[]{ 0, 1, 1, 1, 0, 1, 1, 0 };
        }

        public int[] Board { get { return board; } set { SetProperty(ref board, value); } }
        private int[] board;



    }
}
