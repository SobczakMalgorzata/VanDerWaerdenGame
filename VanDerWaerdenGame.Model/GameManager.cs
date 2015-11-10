using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Model
{
    public class GameManager
    {
        private int endGameLengthCondition;
        public int EndGameLengthCondition
        {
            get { return endGameLengthCondition; }
            set { endGameLengthCondition = value; }
        }

        public GameManager(int endGameLegthCondition)
        {
            this.endGameLengthCondition = endGameLegthCondition;
        }
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
        public static bool DetectProgression(int[] board, int progressionLength, int front, int distance)
        {
            var color = board[front];
            progressionLength--;
            for(int i = front+distance; i < board.Length && progressionLength > 0; progressionLength--)
            {
                if (color != board[i])
                    return false;
                i += distance;
            }
            return true;
        }
    }
}
