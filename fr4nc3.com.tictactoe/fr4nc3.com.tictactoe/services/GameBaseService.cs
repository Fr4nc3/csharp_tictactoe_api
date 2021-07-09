using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.services
{
    /// <summary>
    /// Base Game service with all the common features 
    /// </summary>
    public class GameBaseService
    {
        /// <summary>
        /// getNewMove used to get a new valid position on the gameboard
        /// </summary>
        /// <param name="gameBoard">string array </param>
        /// <returns>null or  next calculate move </returns>
        public int? getNewMove(string[] gameBoard)
        {
            // list of the avaliable position on the gameborad
            var list = availablePositions(gameBoard);
            if (list.Length == 0) // no more available moviments
            {
                return null;
            }
            Random rInt = new Random();
            return list[rInt.Next(0, list.Length)]; // pic one random
        }
        /// <summary>
        /// First Movement
        /// </summary>
        /// <returns>return a value from 9 to 8 all the field of the board ara available</returns>
        public int firstMove()
        {
            Random r = new Random();
            return r.Next(0, 8);

        }
        /// <summary>
        /// availablePositions find all the position on the board that player can move
        /// </summary>
        /// <param name="gameBoard">string array</param>
        /// <returns> integer array of all available position</returns>
        public int[] availablePositions(string[] gameBoard)
        {
            var list = new List<int>(); // easy to use list
            int index = 0;
            foreach (var item in gameBoard)
            {
                if (item == "?") // if position is avaliable
                {
                    list.Add(index);
                }
                ++index;
            }

            return list.ToArray(); // return as array
        }

        /// <summary>
        /// checkWinner check if there is a winner in the gameboard
        /// </summary>
        /// <param name="gameBoard">string array </param>
        /// <param name="player">player that will be tested for winner X or O</param>
        /// <returns>return a valid winner position according the player, empty array if no winner</returns>
        public int[] checkWinner(string[] gameBoard, string player)
        {
            // check the 8 possible combination for winning tictactoe
            // this is very primitive winning check 
            if (gameBoard[0] == gameBoard[1] && gameBoard[1] == gameBoard[2] && gameBoard[2] == player)
            {
                return new int[] { 0, 1, 2 };
            }
            if (gameBoard[3] == gameBoard[4] && gameBoard[4] == gameBoard[5] && gameBoard[5] == player)
            {
                return new int[] { 3, 4, 5 };
            }
            if (gameBoard[6] == gameBoard[7] && gameBoard[7] == gameBoard[8] && gameBoard[8] == player)
            {
                return new int[] { 6, 7, 8 };
            }
            if (gameBoard[0] == gameBoard[4] && gameBoard[4] == gameBoard[8] && gameBoard[8] == player)
            {
                return new int[] { 0, 4, 8 };
            }
            if (gameBoard[2] == gameBoard[4] && gameBoard[4] == gameBoard[6] && gameBoard[6] == player)
            {
                return new int[] { 2, 4, 6 };
            }
            if (gameBoard[0] == gameBoard[3] && gameBoard[3] == gameBoard[6] && gameBoard[6] == player)
            {
                return new int[] { 0, 3, 6 };
            }
            if (gameBoard[1] == gameBoard[4] && gameBoard[4] == gameBoard[7] && gameBoard[7] == player)
            {
                return new int[] { 1, 4, 7 };
            }
            if (gameBoard[2] == gameBoard[5] && gameBoard[5] == gameBoard[8] && gameBoard[8] == player)
            {
                return new int[] { 2, 5, 8 };
            }
            // no winner positions
            return new int[] { };
        }

    }
}
