using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Model for the SelfMoveResponse extends SelfMove
    /// </summary>
    public class SelfMoveResponse: SelfMove
    {
        /// <summary>
        /// move integer 
        /// </summary>
        /// <remarks>
        /// return null 0 to 8 integer
        /// </remarks>
        /// <value>
        /// int or null
        /// </value>
        public int? move { get; set; }
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
        /// array of integers
        /// </value>
        public int[] winPositions { get; set; }
    }
}
