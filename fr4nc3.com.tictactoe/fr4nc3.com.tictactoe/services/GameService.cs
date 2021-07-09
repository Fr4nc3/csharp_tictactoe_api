using fr4nc3.com.tictactoe.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.services
{
    public class GameService : GameBaseService
    {
        private GameMove gameMove;
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="gameMove"> gameMove Object</param>
        public GameService(GameMove gameMove)
        {
            // set the gameMove Object from body
            this.gameMove = gameMove;
        }
        /// <summary>
        ///  play method that execute the  game mode
        /// </summary>
        /// <returns> GameMoveResponse object</returns>
        public GameMoveResponse play()
        {
            // initialize the new GameMoveResponse
            var gameResponse = new GameMoveResponse()
            {
                azurePlayerSymbol = this.gameMove.azurePlayerSymbol,
                humanPlayerSymbol = this.gameMove.humanPlayerSymbol,
                gameBoard = this.gameMove.gameBoard
            };
            // if it is the first move we get a random position 
            if (this.gameMove.isFirstMove)
            {
                int rInt = firstMove();
                gameResponse.move = rInt;
                gameResponse.gameBoard[rInt] = this.gameMove.azurePlayerSymbol;
                gameResponse.winner = Enum.GetName(typeof(Winner), Winner.Inconclusive);

            }
            else
            {
                // we check if there game is finished and there is a winner for human player
                var checkHumanWin = checkWinner(this.gameMove.gameBoard, this.gameMove.humanPlayerSymbol);
                if (checkHumanWin.Length > 0)
                {
                    gameResponse.winner = this.gameMove.humanPlayerSymbol;
                    gameResponse.winPositions = checkHumanWin;
                    return gameResponse; // the board already have a winner no more calculation
                }
                var checkAsureWin = checkWinner(gameResponse.gameBoard, gameResponse.azurePlayerSymbol);
                if (checkAsureWin.Length > 0)
                {
                    gameResponse.winner = gameResponse.azurePlayerSymbol;
                    gameResponse.winPositions = checkAsureWin;
                    return gameResponse; // the board already have a winner no more calculation
                }

                // if we can play we try to get a new position for azure player
                int? azureMove = getNewMove(this.gameMove.gameBoard);
                if (azureMove == null) // no more movements 
                {
                    gameResponse.winner = Enum.GetName(typeof(Winner), Winner.Tie);
                }
                else
                {
                    // we apply the new movement and we check if there is a winner
                    gameResponse.move = azureMove;
                    gameResponse.gameBoard[(int)azureMove] = gameResponse.azurePlayerSymbol;
                    checkAsureWin = checkWinner(gameResponse.gameBoard, gameResponse.azurePlayerSymbol);
                    if (checkAsureWin.Length > 0)
                    {
                        gameResponse.winner = gameResponse.azurePlayerSymbol;
                        gameResponse.winPositions = checkAsureWin;
                    }
                    else
                    {
                        var moreMoves = availablePositions(gameResponse.gameBoard);
                        gameResponse.winner = moreMoves.Length == 0 ? Enum.GetName(typeof(Winner), Winner.Tie) : Enum.GetName(typeof(Winner), Winner.Inconclusive);

                    }
                }

            }
            return gameResponse;
        }
    }
}
