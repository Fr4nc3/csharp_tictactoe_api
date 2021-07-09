using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Model for the GameMove response extends GameMove
    /// </summary>
    public class GameMoveResponse : GameMove
    {
        /// <summary>
        /// winner string
        /// </summary>
        /// <remarks>
        /// return tie X O or Inconclusive
        /// </remarks>
        /// <value>
        /// string 
        /// </value>
        public string winner { get; set; }
        /// <summary>
        /// winPositions array or null
        /// </summary>
        /// <remarks>
        /// return an array of the winner fields or null of no winner
        /// </remarks>
        /// <value>
        /// int array
        /// </value>tie X O or Inconclusive
        public int[] winPositions { get; set; }

    }
}
