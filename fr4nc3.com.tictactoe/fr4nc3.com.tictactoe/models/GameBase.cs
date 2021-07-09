using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Game Base Model common fields betwen Game Movels
    /// </summary>
    public class GameBase
    {

        /// <summary>
        /// gameBoard string array
        /// </summary>
        /// <remarks>
        /// String array of 9 lenght 
        /// </remarks>
        /// <value>
        /// array of strings
        /// </value>
        [Required, MaxLength(9), MinLength(9)]
        public string[] gameBoard { get; set; }

        /// <summary>
        /// validateBoard  bool internal field
        /// </summary>
        /// <remarks>
        /// true if the array is fild with X O or ?
        /// </remarks>
        /// <value>
        /// bool
        /// </value>
        [JsonIgnore]
        [Range(typeof(bool), "true", "true", ErrorMessage = "gameBoard only accepts O, X, ?")]
        public bool validateBoard => Array.TrueForAll(gameBoard, x => x == "O" || x == "X" || x == "?");

        /// <summary>
        /// isFirstMove  bool internal field
        /// </summary>
        /// <remarks>
        /// true if all the fields are ? otherwise false
        /// </remarks>
        /// <value>
        /// bool
        /// </value>
        [JsonIgnore]
        public bool isFirstMove => Array.TrueForAll(gameBoard, x => x == "?");

        /// <summary>
        /// validateBoardMove  bool internal field
        /// </summary>
        /// <remarks>
        /// true  valid moves
        /// </remarks>
        /// <value>
        /// bool
        /// </value>
        [JsonIgnore]
        [Range(typeof(bool), "true", "true", ErrorMessage = "gameBoard invalid")]
        public bool validateBoardMove => isFirstMove || gameBoard.Count(x => x == "X") == gameBoard.Count(x => x == "O") ? true :
           (
            //gameBoard.Count(x => x == "X") > gameBoard.Count(x => x == "O") ? gameBoard.Count(x => x == "X") - 1 == gameBoard.Count(x => x == "O")
            //: gameBoard.Count(x => x == "O") - 1 == gameBoard.Count(x => x == "X")

             !(gameBoard.Count(x => x == "X") == 3 && gameBoard.Count(x => x == "O") == 1) && !(gameBoard.Count(x => x == "O") == 3 && gameBoard.Count(x => x == "X") == 1)

           );


    }
}
