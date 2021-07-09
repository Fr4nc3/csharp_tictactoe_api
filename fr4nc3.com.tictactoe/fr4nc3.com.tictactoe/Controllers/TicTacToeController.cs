using fr4nc3.com.tictactoe.models;
using fr4nc3.com.tictactoe.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.Controllers
{
    /// <summary>
    /// Main controller for the TicTacToe Rest
    /// </summary>
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {

        /// <summary>
        /// Logger Instance
        /// </summary>
        private ILogger<TicTacToeController> _logger;
        public TicTacToeController(ILogger<TicTacToeController> logger)
        {
            _logger = logger;
        }
            /// <summary>
            /// executemode route 
            /// </summary>
            /// <param name="gameMove"> payload body object</param>
            /// <remarks>
            /// Sample value of body
            /// 
            /// {
            ///  "move": 2,
            ///  "azurePlayerSymbol": "X",
            ///  "humanPlayerSymbol": "O",
            ///  "gameBoard": [
            ///    "?",  "?",  "O",  "X",  "?",  "X",  "X",  "O", "O"
            ///    ]
            /// }
            ///     
            /// </remarks>
            /// <returns> GameMoveResponse object </returns>
        [Route("executemove")]
        [HttpPost]
        [ProducesResponseType(typeof(GameMoveResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)] // Tells swagger that the response format will be an int for a BadRequest (400)
        public GameMoveResponse ExecuteMove([FromBody] GameMove gameMove)
        {
            // game service initilized
            var gameService = new GameService(gameMove);
            // game play the game
            var result = gameService.play();
            _logger.LogDebug("DebugLine", result);
            _logger.LogInformation("InformationLine", result);
            return result;
        }
        /// <summary>
        /// calculate move
        /// </summary>
        /// <param name="selfMove">payload body object </param>
        /// <remarks>
        /// Sample value of body
        /// 
        /// {
        ///  "playerSymbol": "X",
        ///  "gameBoard": [
        ///    "?",  "?",  "O",  "X",  "?",  "X",  "X",  "O", "O"
        ///    ]
        /// }
        ///     
        /// </remarks>
        /// <returns>SelfMoveResponse object</returns>
        [Route("calculatemove")]
        [HttpPost]
        [ProducesResponseType(typeof(SelfMoveResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)] // Tells swagger that the response format will be an int for a BadRequest (400)
        public SelfMoveResponse CalculateMove([FromBody] SelfMove selfMove)
        {
            // game service initialized
            var selfGameService = new SelfGameService(selfMove);
            // self game play 
            var result = selfGameService.play();
            _logger.LogDebug("DebugLine", result);
            _logger.LogInformation("InformationLine", result);
            return result;
        }
    }
}
