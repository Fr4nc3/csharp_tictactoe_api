using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.models
{
    /// <summary>
    /// Player Enum X or O
    /// </summary>
    public enum Player
    {
        [Description("X")]
        X = 1,
        [Description("O")]
        O = 2
    }
    /// <summary>
    /// Winner Enum X O Tie or Inconclusive
    /// </summary>
    public enum Winner
    {
        [Description("X")]
        X = 1,
        [Description("O")]
        O = 2,
        [Description("Tie")]
        Tie = 3,
        [Description("Inconclusive")]
        Inconclusive = 4
    }
}
