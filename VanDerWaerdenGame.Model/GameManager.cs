using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

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
        public void PlayTillEnd()
        {
            GameFinished = false;
            while (!Rules.IsFinalStateOfGame(this.Board.ToArray()))
            {
                IterateTurn();
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
                var nextPosition = player1.GetPosition(board.ToArray());
                var nextColor = player2.GetColor(new BoardState(board.ToArray(), nextPosition));
                lock (_lock)
                {
                    Board.Insert(nextPosition, nextColor);
                }
            }
            Thread.Sleep(500);
            if (restoreBool) GameFinished = true;
        }
    }
}
