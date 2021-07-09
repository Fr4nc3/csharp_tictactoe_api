using Microsoft.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestClientSDKLibrary;
using RestClientSDKLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FunctionalTestProject
{
    /// <summary>
    /// Test cases for executemove tictactoe
    /// </summary>
    [TestClass]
    public class FuntionalTests
    {
        // DEMO: Local testing

        /// <summary>
        ///  API endpoint
        /// </summary>
        const string EndpointUrlString = "https://localhost:5001/";
        /// <summary>
        /// Cliente Service credential
        /// </summary>
        public ServiceClientCredentials serviceClientCredentials;
        /// <summary>
        /// RestClientSDKLibraryClient
        /// </summary>
        public RestClientSDKLibraryClient client;
        /// <summary>
        /// GameMode Object
        /// </summary>
        public GameMove gameMove;
        /// <summary>
        /// SelfMOve Object
        /// </summary>
        public SelfMove selfMove;
        //Testing against Azure instance
        //const string EndpointUrlString = "";

        /// <summary>
        /// initilize the variables used on all the test cases
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            serviceClientCredentials = new TokenCredentials("FakeTokenValue");
            client = new RestClientSDKLibraryClient(new Uri(EndpointUrlString), serviceClientCredentials);
        }
        /// <summary>
        /// Test human player win 
        /// </summary>
        /// <returns>success</returns>
        [TestMethod]
        public async Task TestPostPlayerOWin()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "O", "O", "X", "?", "X", "X", "O", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);

            GameMoveResponse resultPayload = resultObject as GameMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.Winner.Contains("O"));
                Assert.IsNull(resultPayload.Move);
            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }
        /// <summary>
        /// Test Azure player win
        /// </summary>
        /// <returns> sucess</returns>
        [TestMethod]
        public async Task TestPostPlayerXWin()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "X", "O", "X", "?", "X", "X", "O", "O" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.Winner.Contains("X"));
                Assert.IsTrue(4 == (int)resultPayload.Move);
            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }
        /// <summary>
        /// Test No winner end game
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostTieGame()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "O", "O", "X", "O", "X", "O", "X" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.Winner.Contains("Tie"));
                Assert.IsTrue(1 == (int)resultPayload.Move);
            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }
        /// <summary>
        /// Test inconcluse result still movements
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostInconcluseGame()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;
            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.Winner.Contains("Inconclusive"));

            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }

        /// <summary>
        /// Test Player are different symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostPlayerXandPlayerO()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;
            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.AzurePlayerSymbol == "X");
                Assert.IsTrue(resultPayload.HumanPlayerSymbol == "O");

            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }
        /// <summary>
        /// Test Player differnt symbol inverted
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostPlayerOandPlayerX()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "X", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;
            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.AzurePlayerSymbol == "O");
                Assert.IsTrue(resultPayload.HumanPlayerSymbol == "X");

            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
        }
        /// <summary>
        /// Test Error Player same symbol X
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPlayersSameSymbolXGame()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "X", "O", "?", "?", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            //Asssert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Player same Symbol O
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPlayersSameSymbolOGame()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "X", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error Azure Wrong Symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadPlayerSymbolAzure()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "P",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "X", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorPlayer Human wrong Symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadPlayerSymbolHuman()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "L",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad board moves
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadBoardMovesX()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "X", "X", "X", "O", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad board moves
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadBoardMovesO()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "?", "X", "", "O", "?", "?", "O", "?", "O" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error GameBoard wrong symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadGameBoardSymbol()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "O", "?", "?", "H", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error Gameboard with more elements
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestBadGameBoardTooBig()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "O", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error GameBoard TooSmall
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostBadBoardGameTooSmall()
        {
            //Arrange  
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error Move out of range positive
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostBadMoveOutRange()
        {
            //Arrange  
            gameMove = new GameMove()
            {
                Move = 15,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "X", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Error Move out of range negative
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostBadMoveOutRangeNeg()
        {
            //Arrange  
            gameMove = new GameMove()
            {
                Move = -1,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "?", "?", "?", "O", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Good board
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostGoodBoardGame()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "O", "?", "O", "X", "?", "X", "X", "O", "O" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            Console.WriteLine(resultObject);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.AzurePlayerSymbol.Contains("X"));
            }
            else
            {
                Assert.Fail("Expected a  GameMoveResponse but didn't recieve one");
            }

        }
        /// <summary>
        /// Test First move
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostGameBoardFirstMove()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveAsync(body: gameMove);
            Console.WriteLine(resultObject);
            GameMoveResponse resultPayload = resultObject as GameMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.GameBoard.Count(x => x == "X") == 1);
            }
            else
            {
                Assert.Fail("Expected a  GameMoveResponse but didn't recieve one");
            }

        }
        /// <summary>
        /// Test Error Bad Game Board
        /// </summary>
        /// <returns>Sucess</returns>
        [TestMethod]
        public async Task TestPostGameBoardBadMoves()
        {
            //Arrange   
            gameMove = new GameMove()
            {
                Move = 0,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() { "X", "?", "?", "X", "X", "?", "O", "?", "?" }
            };

            // Act
            var resultObject = await client.ExecuteMoveWithHttpMessagesAsync(body: gameMove);
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);

        }
        /// <summary>
        /// Test First move
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestPostSelfFirstMove()
        {
            //Arrange   
            selfMove = new SelfMove()
            {
                PlayerSymbol = "X",
                GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.CalculateMoveAsync(body: selfMove);
            Console.WriteLine(resultObject);
            SelfMoveResponse resultPayload = resultObject as SelfMoveResponse;

            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.GameBoard.Count(x => x == "X") == 1);
            }
            else
            {
                Assert.Fail("Expected a  GameMoveResponse but didn't recieve one");
            }

        }
        /// <summary>
        /// Test ErrorBad board moves
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestBadBoardSelfO()
        {
            //Arrange   
            selfMove = new SelfMove()
            {

                PlayerSymbol = "O",
                GameBoard = new List<string>() { "?", "X", "?", "O", "?", "?", "O", "?", "O" }
            };

            // Act
            var resultObject = await client.CalculateMoveWithHttpMessagesAsync(body: selfMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad board small array
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestBadBoardTooSmall()
        {
            //Arrange   
            selfMove = new SelfMove()
            {

                PlayerSymbol = "O",
                GameBoard = new List<string>() { "?", "X", "?", "O", "?"}
            };

            // Act
            var resultObject = await client.CalculateMoveWithHttpMessagesAsync(body: selfMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad board small array
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestBadBoardTooBig()
        {
            //Arrange   
            selfMove = new SelfMove()
            {

                PlayerSymbol = "O",
                GameBoard = new List<string>() { "?", "X", "?", "O", "?", "?", "X", "?", "O", "?", "?", "X", "?", "O", "?" }
            };

            // Act
            var resultObject = await client.CalculateMoveWithHttpMessagesAsync(body: selfMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad player symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestBadSelfPlayerSymbol()
        {
            //Arrange   
            selfMove = new SelfMove()
            {

                PlayerSymbol = "R",
                GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.CalculateMoveWithHttpMessagesAsync(body: selfMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test ErrorBad player symbol
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestBadSelfBoardSymbol()
        {
            //Arrange   
            selfMove = new SelfMove()
            {

                PlayerSymbol = "R",
                GameBoard = new List<string>() { "?", "F", "?", "?", "?", "X", "?", "?", "O" }
            };

            // Act
            var resultObject = await client.CalculateMoveWithHttpMessagesAsync(body: selfMove);
            //Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)resultObject.Response.StatusCode);
        }
        /// <summary>
        /// Test Self play run as debug to see output
        /// </summary>
        /// <returns>Sucess</returns>
        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestPostSelfPlay()
        {
            Debug.WriteLine("Enter TestPostSelfPlay: {0}", DateTime.Now);

            //Arrange   
            // first movement 
            selfMove = new SelfMove()
            {

                PlayerSymbol = "X",
                GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" }
            };

            // Act
            var resultObject = await client.CalculateMoveAsync(body: selfMove);
            SelfMoveResponse resultPayload = resultObject as SelfMoveResponse;
            // Assert
            if (resultPayload != null)
            {
                Assert.IsTrue(resultPayload.PlayerSymbol == "X");
                Debug.Write("Get First Movement: ");
                Debug.Write(String.Join(", ", resultPayload.GameBoard.ToArray()));
                Debug.WriteLine(" ");
                var count = 1; // 
                while (resultPayload.Winner == "Inconclusive") // always enter in first move
                {
                    Assert.IsTrue(resultPayload.Winner == "Inconclusive");
                    selfMove.GameBoard = resultPayload.GameBoard; // use new board
                    selfMove.PlayerSymbol = resultPayload.PlayerSymbol == "X" ? "O" : "X"; // move to the other player
                    resultObject = await client.CalculateMoveAsync(body: selfMove);
                    resultPayload = resultObject as SelfMoveResponse;
                    Debug.Write("Loop Player: ");
                    Debug.Write(resultPayload.PlayerSymbol);
                    Debug.WriteLine(" ");
                    Debug.Write("Loop GameBoard: ");
                    Debug.Write(String.Join(", ", resultPayload.GameBoard.ToArray()));
                    Debug.WriteLine(" ");
                    Debug.Write("inside winner status: ");
                    Debug.Write(resultPayload.Winner);
                    Debug.WriteLine(" ");
                    ++count;
                }
                Debug.WriteLine("------\n\n");
                Debug.Write("n moves: ");
                Debug.Write(count);
                Debug.WriteLine(" ");
                Debug.Write("winner: ");
                Debug.Write(resultPayload.Winner);
                Debug.WriteLine(" ");
                Debug.Write("winPossitions: ");
                if (resultPayload.WinPositions != null)
                {
                    Debug.Write(String.Join(", ", resultPayload.WinPositions.ToArray()));
                }
                Debug.WriteLine(" ");
                //
                Assert.IsTrue(resultPayload.Winner != "Inconclusive");
            }
            else
            {
                Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
            }
            Debug.WriteLine("ENd TestPostSelfPlay: {0}", DateTime.Now);
        }

        [TestCategory("Extra")]
        [TestMethod]
        public async Task TestPostSelfPlayLoop()
        {
            Debug.WriteLine("Enter TestPostSelfPlay: {0}", DateTime.Now);

            var winnerO = 0;
            var winnerX = 0;
            var winnerTie = 0;
            foreach (int value in Enumerable.Range(1, 100)) //  number of times run the test
            {

                //Arrange   
                // first movement 
                selfMove = new SelfMove()
                {

                    PlayerSymbol = "X",
                    GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" }
                };

                // Act
                var resultObject = await client.CalculateMoveAsync(body: selfMove);
                SelfMoveResponse resultPayload = resultObject as SelfMoveResponse;
                // Assert
                if (resultPayload != null)
                {
                    Assert.IsTrue(resultPayload.PlayerSymbol == "X");

                    var count = 1; // 
                    while (resultPayload.Winner == "Inconclusive") // always enter in first move
                    {
                        Assert.IsTrue(resultPayload.Winner == "Inconclusive");
                        selfMove.GameBoard = resultPayload.GameBoard; // use new board
                        selfMove.PlayerSymbol = resultPayload.PlayerSymbol == "X" ? "O" : "X"; // move to the other player
                        resultObject = await client.CalculateMoveAsync(body: selfMove);
                        resultPayload = resultObject as SelfMoveResponse;

                        ++count;
                    }
                    if(resultPayload.Winner == "X")
                    {
                        ++winnerX;
                    }
                    if (resultPayload.Winner == "O")
                    {
                        ++winnerO;
                    }
                    if (resultPayload.Winner == "Tie")
                    {
                        ++winnerTie;
                    }
                    if (resultPayload.WinPositions != null)
                    {
                        Debug.Write(String.Join(", ", resultPayload.WinPositions.ToArray()));
                    }
                    Debug.WriteLine(" ");
                    //
                    Assert.IsTrue(resultPayload.Winner != "Inconclusive");
                }
                else
                {
                    Assert.Fail("Expected a to create gameMoveResponse but didn't recieve one");
                }
                
            } // foreach
            Debug.WriteLine(" ");
            Debug.Write("winnerX: ");
            Debug.Write(winnerX);
            Debug.WriteLine(" ");
            Debug.Write("winnerO: ");
            Debug.Write(winnerO);
            Debug.WriteLine(" ");
            Debug.Write("winnerTie: ");
            Debug.Write(winnerTie);
            Debug.WriteLine(" ");
            Debug.WriteLine("ENd foreach lopp: {0}", DateTime.Now);
        }
    }
}
