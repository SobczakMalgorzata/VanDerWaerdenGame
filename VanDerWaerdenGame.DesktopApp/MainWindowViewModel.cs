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
using System.IO;

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

        public void StartNewGame() { Task.Factory.StartNew(GameManager.NewGame); }
        public void Turn() { Task.Factory.StartNew(GameManager.IterateTurn); }
        public void PlayTillEnd() { Task.Factory.StartNew(() => GameManager.PlayTillEnd(true)); }

        public bool ShouldTrainP1 { get { return shouldTrainP1; } set { SetProperty(ref shouldTrainP1, value); } }
        private bool shouldTrainP1 = true;
        //public bool ShouldTrainP2 { get { return shouldTrainP2; } set { SetProperty(ref shouldTrainP2, value); } }
        //private bool shouldTrainP2;
        public double P1Efficiency { get { return p1Efficiency; } set { SetProperty(ref p1Efficiency, value); } }
        private double p1Efficiency;
        //public double P2Efficiency { get { return p2Efficiency; } set { SetProperty(ref p2Efficiency, value); } }
        //private double p2Efficiency;
        public bool IsTraining { get { return isTraining; } set { SetProperty(ref isTraining, value); } }
        private bool isTraining;
        
        public int NTestingIterations { get { return nTrainingIterations; } set { SetProperty(ref nTrainingIterations, value); } }
        private int nTrainingIterations = 5000;
        public int TrainingIteration { get { return trainingIteration; } set { SetProperty(ref trainingIteration, value); } }
        private int trainingIteration = 0;

        public double StepSize { get { return stepSize; } set { SetProperty(ref stepSize, value); } }
        private double stepSize = 0.01;
        public int NStepGames { get { return nStepGames; } set { SetProperty(ref nStepGames, value); } }
        private int nStepGames = 100;
        
        public void TrainPositionPlayer()
        {
            IsTraining = true;

            TrainingIteration = 0;
            var nSteps = (int)Math.Ceiling(1.0 / StepSize);

            ITrainable P1 = GameManager.Player1 as ITrainable;
            var P1Trainer = new PositionStepTrainer(GameManager.Rules);
            NeuralSimulatedAnnealing training = null;

            if (P1 != null)
            {
                training = new NeuralSimulatedAnnealing(P1.Network, P1Trainer, 20, 2, nSteps * NStepGames);

                if (ShouldTrainP1)
                    for (int i = 0; i < nSteps; i++)
                    {
                        P1Trainer.Distortion = 1 - i * StepSize;
                        for (int j = 0; j < NStepGames; j++)
                        {
                            training.Iteration();
                            TrainingIteration++;
                        }
                    }
            }
            IsTraining = false;
        }

        public void TestPlayers(string fileName = null)
        {
            var gm = new GameManager(this.GameManager.Rules) {
                Player1 = GameManager.Player1,
                Player2 = GameManager.Player2,
            };
            if (fileName != null)
                gm.Logger = new AppendGameLogger(fileName);

            List<int> gameLengths = new List<int>();
            for (int i = 0; i < NTestingIterations; i++)
            {
                //gm.NewGame();
                gameLengths.Add(gm.PlayGame());
            }
            P1Efficiency = gameLengths.Average();
        }
    }
   
}
