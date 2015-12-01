using Encog.Neural.Networks.Training;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Encog.ML;

namespace VanDerWaerdenGame.Model
{
    public class GameManager : BindableBase
    {
        public ObservableCollection<int> Board { get { return board; }  private set { SetProperty(ref board, value); } }
        private ObservableCollection<int> board;
        private object _lock = new object();

        public IGameRules Rules { get { return rules; } set { SetProperty(ref rules, value); } }
        private IGameRules rules;
        public IPositionPlayer Player1 { get { return player1; } set { SetProperty(ref player1, value); } }
        private IPositionPlayer player1;
        public IColorPlayer Player2 { get { return player2; } set { SetProperty(ref player2, value); } }
        private IColorPlayer player2;
        public bool GameFinished { get { return gameFinished; } set { SetProperty(ref gameFinished, value); } }


        private bool gameFinished = false;
        
        public GameManager(IGameRules rules)
        {
            Board = new ObservableCollection<int>();
            Rules = rules;
            BindingOperations.EnableCollectionSynchronization(board, _lock);
            this.GameFinished = true;
        }

        /// <summary>
        /// Starts the game loop.
        /// </summary>
        public void NewGame()
        {
            lock (_lock)
            {
                Board.Clear();
            }
        }
        
        /// <summary>
        /// Makes moves till the game ends.
        /// </summary>
        public void PlayTillEnd(bool visible)
        {
            GameFinished = false;
            while (!Rules.IsFinalStateOfGame(this.Board.ToArray()))
            {
                IterateTurn();
                if(visible)
                    Thread.Sleep(500);
            }
            GameFinished = true;
        }

        /// <summary>
        /// Makes one move of each of players.
        /// </summary>
        public void IterateTurn()
        {
            var restoreBool = GameFinished;
            if (restoreBool) GameFinished = false;
            if (!Rules.IsFinalStateOfGame(Board.ToArray()))
            {
                MakeMove();
            }
            if (restoreBool) GameFinished = true;
        }

        public void MakeMove()
        {
            var restoreBool = GameFinished;
            if (restoreBool) GameFinished = false;

            if (!Rules.IsFinalStateOfGame(Board.ToArray()))
            {
                lock (_lock)
                {
                    if (Board.Any(x => x == -1))
                    {
                        var nextPosition = Board.IndexOf(-1);
                        var nextColor = player2.GetColor(new BoardState(Board.ToArray(), nextPosition));
                        Board[nextPosition] = nextColor;
                    }
                    else
                    {
                        var nextPosition = player1.GetPosition(Board.ToArray());
                        Board.Insert(nextPosition, -1);
                    }
                }
            }

            if (restoreBool) GameFinished = true;
        }

        public int PlayGame()
        {
            NewGame();
            PlayTillEnd(false);
            return Board.Count();
        }
    }
}
