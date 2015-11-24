using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Model
{
    public class BoardState : BindableBase
    {
        /// <summary>
        /// Array of dynamic length representing state of dots.
        /// True - blue
        /// False - red
        /// </summary>
        private int[] boardColors;
        public int[] BoardColors { get { return boardColors; } set { SetProperty(ref boardColors, value); } }
        
        /// <summary>
        /// Position picked by Player 1
        /// -1 - Player 1 move
        /// {0-9} - Player 2 move
        /// </summary>
        private int selectedPosition;
        public int SelectedPosition { get { return selectedPosition; } set { SetProperty(ref selectedPosition, value); } }

        /// <summary>
        /// Default constructor with empty board and waiting for move from Player 2.
        /// </summary>
        public BoardState() : this(new int[0], 0) { }

        /// <summary>
        /// Creates Board state with given parameters.
        /// </summary>
        /// <param name="board">Array with numbers symboling colors.</param>
        /// <param name="selectedPosition">Position picked by Player 1.</param>
        public BoardState(int[] board, int selectedPosition = -1) 
        {
            this.BoardColors = board;
            this.SelectedPosition = selectedPosition;
        }

    }
}
