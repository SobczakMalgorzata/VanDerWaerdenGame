using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanDerWaerdenGame.Model;

namespace VanDerWaerdenGame.Players.ColorChoosers
{
    public class MeanColorPlayer : ColorPlayerBase
    {
        private static int counter = (int)DateTime.Now.Ticks;
        public MeanColorPlayer(VanDerWaerdenGameRules rules) : base(rules) { }
        public double Distortion { get { return distortion; } set { SetProperty(ref distortion, value); } }
        private double  distortion = 0;
        public override string PlayerName { get { return "Mean Player"; } }
        public override int GetColor(BoardState board)
        {
            var rand = new Random(counter);

            if (rand.NextDouble() <= Distortion)
                return rand.Next(Rules.NColors);

            List<int> possibleMoves = new List<int>();
            int position = board.SelectedPosition;
            for (int color = 0; color < Rules.NColors; color++)
            {
                board.BoardColors[position] = color;
                if (!Rules.IsFinalStateOfGame(board.BoardColors))
                    possibleMoves.Add(color);
            }
            counter = ++counter % int.MaxValue;
            if (possibleMoves.Count < 1)
                return rand.Next(Rules.NColors);
            return possibleMoves[rand.Next(possibleMoves.Count)];
        }
    }
}
