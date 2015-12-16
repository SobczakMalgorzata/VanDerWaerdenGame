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
        public MeanColorPlayer(VanDerWaerdenGameRules rules) : base(rules) { }
        public override string PlayerName { get { return "Mean Player"; } }
        public override int GetColor(BoardState board)
        {
            List<int> possibleMoves = new List<int>();
            int position = board.SelectedPosition;
            for (int color = 0; color < Rules.NColors; color++)
            {
                board.BoardColors[position] = color;
                if (!Rules.IsFinalStateOfGame(board.BoardColors))
                    possibleMoves.Add(color);
            }
            var rand = new Random();
            if (possibleMoves.Count < 1)
                return rand.Next(Rules.NColors);
            return possibleMoves[rand.Next(possibleMoves.Count)];
        }
    }
}
