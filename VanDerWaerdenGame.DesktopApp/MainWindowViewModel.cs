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
using VanDerWaerdenGame.Players;
using Encog.Neural.Networks.Training.Anneal;

namespace VanDerWaerdenGame.DesktopApp
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel() : base()
        {
            //Player is loaded just to have static linking ot the Players assembly
            var plr = new RandomColorPlayer(GameManager.Rules as VanDerWaerdenGameRules);

            ColorPlayers = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IColorPlayer).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                        .Select(t => (IColorPlayer)Activator.CreateInstance(t, GameManager.Rules))
                        .ToList();

            PositionPlayers = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IPositionPlayer).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                        .Select(t => (IPositionPlayer)Activator.CreateInstance(t, GameManager.Rules))
                        .ToList();

            foreach (PlayerBase player in ColorPlayers)
                player.Rules = gameManager.Rules as VanDerWaerdenGameRules;
            foreach (PlayerBase player in ColorPlayers)
                player.Rules = gameManager.Rules as VanDerWaerdenGameRules;

            GameManager.Player1 = PositionPlayers.Last();
            GameManager.Player2 = ColorPlayers.Last();
        }

        public List<IColorPlayer> ColorPlayers { get; set; }
        public List<IPositionPlayer> PositionPlayers { get; set; }

        public GameManager GameManager { get { return gameManager; } set { SetProperty(ref gameManager, value); } }
        private GameManager gameManager = new GameManager(new VanDerWaerdenGameRules());

        public void StartNewGame() { this.GameManager.NewGame(); }
        public void Turn() { this.GameManager.IterateTurn(); }
        public void PlayTillEnd() { this.GameManager.PlayTillEnd(true); }

        public bool ShouldTrainP1 { get { return shouldTrainP1; } set { SetProperty(ref shouldTrainP1, value); } }
        private bool shouldTrainP1;
        public bool ShouldTrainP2 { get { return shouldTrainP2; } set { SetProperty(ref shouldTrainP2, value); } }
        private bool shouldTrainP2;
        public int NTrainingIterations { get { return nTrainingIterations; } set { SetProperty(ref nTrainingIterations, value); } }
        private int nTrainingIterations = 200;

        
        public void TrainPlayers()
        {
            if (ShouldTrainP1 && GameManager.Player1 is ITrainable)
                Train(GameManager.Player1 as ITrainable, new PositionPlayerTrainer(GameManager.Rules, gameManager.Player2));
            if (ShouldTrainP2 && GameManager.Player2 is ITrainable)
                Train(GameManager.Player2 as ITrainable, new ColorPlayerTrainer(GameManager.Rules, gameManager.Player1));
        }
        private void Train(ITrainable player, PlayersTrainerBase trainer)
        {
            var train = new NeuralSimulatedAnnealing(
                player.Network, trainer, 10, 2, 200);

            for (int i = 0; i < NTrainingIterations; i++)
                train.Iteration();
        }
    }
   
}
