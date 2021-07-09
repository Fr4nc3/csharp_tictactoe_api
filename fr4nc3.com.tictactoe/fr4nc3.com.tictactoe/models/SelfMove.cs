using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Model for the SelfMove extends GameBase
    /// </summary>
    public class SelfMove : GameBase
    {
        /// <summary>
        /// playerSymbol string
        /// </summary>
        /// <remarks>
        /// string X or O
        /// </remarks>
        /// <value>
        /// string 
        /// </value>
        [RegularExpression(@"^[O,X]{1}"), Required, StringLength(1, MinimumLength = 1)]
        public string playerSymbol { get; set; }
    }
}
