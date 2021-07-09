using fr4nc3.com.tictactoe.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr4nc3.com.tictactoe.services
{
    public class SelfGameService : GameBaseService
    {
        private SelfMove selfMove;
        private string otherPlayerSimbol;
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="selfMove"></param>
        public SelfGameService(SelfMove selfMove)
        {
            // set the selfMove Object from body
            this.selfMove = selfMove;
            // calculate  other player
            this.otherPlayerSimbol = selfMove.playerSymbol == Enum.GetName(typeof(Player), Player.X) ? Enum.GetName(typeof(Player), Player.O) : Enum.GetName(typeof(Player), Player.X);
        }

        /// <summary>
        ///  play method that execute the self game mode
        /// </summary>
        /// <returns> SelfMoveResponse object</returns>
        public SelfMoveResponse play()
        {
            // initialize the new SelfMoveResponse
            var selfResponse = new SelfMoveResponse()
            {
                playerSymbol = this.selfMove.playerSymbol,
                gameBoard = this.selfMove.gameBoard
            };
            // if it is the first move we get a random position 
            if (this.selfMove.isFirstMove)
            {

                int rInt = firstMove();
                selfResponse.move = rInt;
                selfResponse.gameBoard[rInt] = this.selfMove.playerSymbol;
                selfResponse.winner = Enum.GetName(typeof(Winner), Winner.Inconclusive);

            }
            else
            {
                // we check if there game is finished and there is a winner for two players
                var checkPlayerWin = checkWinner(selfResponse.gameBoard, selfResponse.playerSymbol);
                if (checkPlayerWin.Length > 0)
                {
                    selfResponse.winner = selfResponse.playerSymbol;
                    selfResponse.winPositions = checkPlayerWin;
                    return selfResponse; // the board already have a winner no more calculation
                }

                var checkOtherPlayerWin = checkWinner(selfResponse.gameBoard, this.otherPlayerSimbol);
                if (checkOtherPlayerWin.Length > 0)
                {
                    selfResponse.winner = this.otherPlayerSimbol;
                    selfResponse.winPositions = checkOtherPlayerWin;
                    return selfResponse; // the board already have a winner no more calculation
                }
                // if we can play we try to get a new position
                int? newMove = getNewMove(selfResponse.gameBoard);
                if (newMove == null) // no more movements 
                {
                    selfResponse.winner = Enum.GetName(typeof(Winner), Winner.Tie);
                }
                else
                {
                    // we apply the new movement and we check if there is a winner
                    selfResponse.move = newMove;
                    selfResponse.gameBoard[(int)newMove] = selfResponse.playerSymbol;
                    checkPlayerWin = checkWinner(selfResponse.gameBoard, selfResponse.playerSymbol);
                    if (checkPlayerWin.Length > 0)
                    {
                        selfResponse.winner = selfResponse.playerSymbol;
                        selfResponse.winPositions = checkPlayerWin;
                    }
                    else
                    {
                        // we check if we have more movements to decide is tie or inconclusive
                        var moreMoves = availablePositions(selfResponse.gameBoard);
                        selfResponse.winner = moreMoves.Length == 0 ? Enum.GetName(typeof(Winner), Winner.Tie) : Enum.GetName(typeof(Winner), Winner.Inconclusive);
                    }

                }

            }
            return selfResponse;
        }
    }
}
