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

        }

        public GameManager GameManager { get { return gameManager; } set { SetProperty(ref gameManager, value); } }
        private GameManager gameManager = new GameManager(new VanDerWaerdenGameRules()) { Player1 = new RandomPositionPlayer(), Player2 = new RandomColorPlayer() };

        public void StartNewGame()
        {
            this.GameManager.NewGame();
        }

        public void Turn()
        {
            this.GameManager.IterateTurn();
        }

        public void PlayTillEnd() { this.GameManager.PlayTillEnd(); }

    }
}
