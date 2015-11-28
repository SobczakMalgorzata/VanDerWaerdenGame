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
            //Player is loaded just to have static linking ot the Players assembly

            var plr = new RandomColorPlayer();
            
            ColorPlayers = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IColorPlayer).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                        .Select(t => (IColorPlayer)Activator.CreateInstance(t))
                        .ToList();

            PositionPlayers = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IPositionPlayer).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                        .Select(t => (IPositionPlayer)Activator.CreateInstance(t))
                        .ToList();
            GameManager.Player1 = PositionPlayers.Last();
            GameManager.Player2 = ColorPlayers.Last();
        }

        public List<IColorPlayer> ColorPlayers { get; set; }
        public List<IPositionPlayer> PositionPlayers { get; set; }

        public GameManager GameManager { get { return gameManager; } set { SetProperty(ref gameManager, value); } }
        private GameManager gameManager = new GameManager(new VanDerWaerdenGameRules());

        public void StartNewGame() { this.GameManager.NewGame(); }

        public void Turn() { this.GameManager.IterateTurn(); }

        public void PlayTillEnd() { this.GameManager.PlayTillEnd(); }

    }
}
