using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Model for the GameMove extends GameBase
    /// </summary>
    public class GameMove : GameBase
    {
        /// <summary>
        /// move integer 
        /// </summary>
        /// <remarks>
        /// accept null or integer from 0 to 8
        /// </remarks>
        /// <value>
        /// int or null
        /// </value>
        [Range(0, 8, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? move { get; set; }
        /// <summary>
        /// azurePlayerSymbol string
        /// </summary>
        /// <remarks>
        /// string X or O
        /// </remarks>
        [RegularExpression(@"^[O,X]{1}"), Required, StringLength(1, MinimumLength = 1)]
        public string azurePlayerSymbol { get; set; }
        /// <summary>
        /// humanPlayerSymbol string
        /// </summary>
        /// <remarks>
        /// string X or O
        /// </remarks>
        /// <value>
        /// string X or O
        /// </value>
        [RegularExpression(@"^[O,X]{1}"), Required, StringLength(1, MinimumLength = 1)]
        public string humanPlayerSymbol { get; set; }
        /// <summary>
        /// validatePlayers bool internal field 
        /// </summary>
        /// <remarks>
        /// bool true when the player are different false otherwise 
        /// </remarks>
        [JsonIgnore]
        [Range(typeof(bool), "true", "true", ErrorMessage = "azurePlayerSymbol and humanPlayerSymbol must be different ")]
        public bool validatePlayers => !(humanPlayerSymbol == azurePlayerSymbol);

        /// <summary>
        /// validateMove bool internal field 
        /// </summary>
        /// <remarks>
        /// bool true when the move is part of the board  false otherwise 
        /// </remarks>
        /// <value>
        /// bool
        /// </value>
        [JsonIgnore]
        [Range(typeof(bool), "true", "true", ErrorMessage = "invalid position")]
        // if first move we don't check the move value, ie it is ignored
        // if move is not null the board must have already the position marked
        public bool validateMove => isFirstMove ? isFirstMove : (move != null ? gameBoard.ElementAtOrDefault((int)move) == humanPlayerSymbol : true) ;


    }
}
