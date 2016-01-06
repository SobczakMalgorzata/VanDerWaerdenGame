using Encog.ML;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;
using VanDerWaerdenGame.Players.ColorChoosers;
using VanDerWaerdenGame.Players.PositionChoosers;

namespace VanDerWaerdenGame.Players
{
    public abstract class PlayersTrainerBase : BindableBase, ICalculateScore
    {
        public PlayersTrainerBase(IGameRules rules) { this.gameManager = new GameManager(rules); ShouldMinimize = false; RequireSingleThreaded = true; } 

        public bool RequireSingleThreaded { get; }
        public bool ShouldMinimize { get; }

        protected GameManager gameManager;        

        public int NGames { get { return nGames; } set { SetProperty(ref nGames, value); } }
        private int nGames = 1;

        protected abstract double CalculateScore(int[] scores);

        public abstract double CalculateScore(IMLMethod network);
    }

    public class ColorPlayerTrainer : PlayersTrainerBase
    {
        public ColorPlayerTrainer(IGameRules rules, IPositionPlayer opponent) : base(rules)
        {
            gameManager.Player1 = opponent;
        }
        protected override double CalculateScore(int[] scores)
        {
            return gameManager.Rules.CalculateColorPlayerScore(scores);
        }

        public override double CalculateScore(IMLMethod network)
        {
            gameManager.Player2 = new NeuralColorPlayer(gameManager.Rules as VanDerWaerdenGameRules) { Network = network as BasicNetwork };
            var scores = new int[NGames];
            for (int i = 0; i < NGames; i++)
                scores[i] = gameManager.PlayGame();
            return CalculateScore(scores);
        }
    }


    public class PositionPlayerTrainer : PlayersTrainerBase
    {
        public PositionPlayerTrainer(IGameRules rules, IColorPlayer opponent) : base(rules)
        {
            gameManager.Player2 = opponent;
        }
        protected override double CalculateScore(int[] scores)
        {
            return gameManager.Rules.CalculatePositionPlayerScore(scores);
        }

        public override double CalculateScore(IMLMethod network)
        {
            gameManager.Player1 = new NeuralPositionPlayer1(gameManager.Rules as VanDerWaerdenGameRules) { Network = network as BasicNetwork };
            var scores = new int[NGames];
            for (int i = 0; i < NGames; i++)
                scores[i] = gameManager.PlayGame();
            return CalculateScore(scores);
        }
    }

    public class PositionStepTrainer : PlayersTrainerBase
    {
        public PositionStepTrainer(IGameRules rules) : base(rules) { gameManager.Player2 = new MeanColorPlayer(this.gameManager.Rules as VanDerWaerdenGameRules); }
        public double Distortion { get { return distortion; } set { SetProperty(ref distortion, value); } }
        private double distortion;
        
        public override double CalculateScore(IMLMethod network)
        {
            this.gameManager.Player1 = new NeuralPositionPlayer1(gameManager.Rules as VanDerWaerdenGameRules) { Network = network as BasicNetwork };
            ((MeanColorPlayer)this.gameManager.Player2).Distortion = this.Distortion;
            var scores = new int[NGames];
            for (int i = 0; i < NGames; i++)
                scores[i] = gameManager.PlayGame();
            return CalculateScore(scores);
        }

        protected override double CalculateScore(int[] scores)
        {
            return scores.Average();
        }
    }
}
