using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;
using VanDerWaerdenGame.Players.ColorChoosers;
using VanDerWaerdenGame.Players.PositionChoosers;

namespace VanDerWaerdenGame.DesktopApp
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel() : base()
        {
            //Board = new int[]{ 0, 1, 1, 1, 0, 1, 1, 0 };
        }

        public GameManager GameManager { get { return gameManager; } set { SetProperty(ref gameManager, value); } }
        private GameManager gameManager = new GameManager(3) { Player1 = new RandomPositionPlayer(), Player2 = new RandomColorPlayer() };

        public void StartNewGame()
        {
            this.GameManager.StartGame();
        }

        public void Turn()
        {
            this.GameManager.IterateTurn();
        }

        public void PlayTillEnd() { this.GameManager.PlayTillEnd(); }

    }
}
