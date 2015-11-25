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

        public int EndGameLengthCondition { get { return endGameLengthCondition; } set { SetProperty(ref endGameLengthCondition, value); } }
        private int endGameLengthCondition = 3;
        public IPositionPlayer Player1 { get { return player1; } set { SetProperty(ref player1, value); } }
        private IPositionPlayer player1;
        public IColorPlayer Player2 { get { return player2; } set { SetProperty(ref player2, value); } }
        private IColorPlayer player2;
        public bool GameFinished { get { return gameStarted; } set { SetProperty(ref gameStarted, value); } }

        private bool gameStarted = false;
        
        public GameManager(int endGameLengthCondition)
        {
            board = new ObservableCollection<int>();
            BindingOperations.EnableCollectionSynchronization(board, _lock);
            this.endGameLengthCondition = endGameLengthCondition;
            this.GameFinished = false;

        }

        /// <summary>
        /// Starts the game loop.
        /// </summary>
        public async Task StartGame()
        {
            this.GameFinished = false;

            lock (_lock)
            {
                Board.Clear();
            }

           /// this.GameFinished = true;
        }
        
        public void PlayTillEnd()
        {
            while (!DetectProgression(this.Board.ToArray(), EndGameLengthCondition))
            {
                IterateTurn();
            }
        }

        public void IterateTurn()
        {
            if (GameFinished == false)
            {
                var nextPosition = player1.GetPosition(board.ToArray());
                var nextColor = player2.GetColor(new BoardState(board.ToArray(), nextPosition));
                lock (_lock)
                {
                    Board.Insert(nextPosition, nextColor);
                }
            }
            Thread.Sleep(500);
        }

        /// <summary>
        /// Detects if there exists a progression in board of length progressionLength.
        /// </summary>
        /// <param name="board">Board to be checked.</param>
        /// <param name="progressionLength">Number of elements in progression.</param>
        /// <returns></returns>
        public static bool DetectProgression(int[] board, int progressionLength)
        {
            int distance = 0;
            int distanceLimit = (board.Count()/progressionLength)+1;
            for(distance = 1; distance <= distanceLimit; distance++)
            {
                for (int front = 0; front < board.Count() - distance * (progressionLength-1); front++)
                {
                    if (DetectProgression(board, progressionLength, front, distance))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if there is a progression of progressionLength elements starting from front with distance between elements in board.
        /// </summary>
        /// <param name="board">Board to be checked.</param>
        /// <param name="progressionLength">Number of elements in progression.</param>
        /// <param name="front">Index of starting element.</param>
        /// <param name="distance">Distance from one element to another. 1 means that elements are next to each other.</param>
        /// <returns></returns>
        public static bool DetectProgression(int[] board, int progressionLength, int front, int distance)
        {
            var color = board[front];
            progressionLength--;
            for(int i = front+distance; i < board.Count() && progressionLength > 0; progressionLength--)
            {
                if (color != board[i])
                    return false;
                i += distance;
            }
            return true;
        }
    }
}
