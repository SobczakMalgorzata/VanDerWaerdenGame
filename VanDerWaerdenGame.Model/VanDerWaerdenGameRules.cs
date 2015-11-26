using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Model
{
    public class VanDerWaerdenGameRules : BindableBase, IGameRules
    {
        public int EndGameProgressionLength { get { return endGameProgressionLength; } set { SetProperty(ref endGameProgressionLength, value); } }
        private int endGameProgressionLength = 3;

        public bool IsFinalStateOfGame(int[] board)
        {
            return DetectProgression(board, EndGameProgressionLength);
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
            int distanceLimit = (board.Count() / progressionLength) + 1;
            for (distance = 1; distance <= distanceLimit; distance++)
            {
                for (int front = 0; front < board.Count() - distance * (progressionLength - 1); front++)
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
            for (int i = front + distance; i < board.Count() && progressionLength > 0; progressionLength--)
            {
                if (color != board[i])
                    return false;
                i += distance;
            }
            return true;
        }
    }
}
