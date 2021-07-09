﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace RestClientSDKLibrary.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// Model for the GameMove response extends GameMove
    /// </summary>
    public partial class GameMoveResponse
    {
        /// <summary>
        /// Initializes a new instance of the GameMoveResponse class.
        /// </summary>
        public GameMoveResponse() { }

        /// <summary>
        /// Initializes a new instance of the GameMoveResponse class.
        /// </summary>
        public GameMoveResponse(IList<string> gameBoard, string azurePlayerSymbol, string humanPlayerSymbol, int? move = default(int?), string winner = default(string), IList<int?> winPositions = default(IList<int?>))
        {
            GameBoard = gameBoard;
            Move = move;
            AzurePlayerSymbol = azurePlayerSymbol;
            HumanPlayerSymbol = humanPlayerSymbol;
            Winner = winner;
            WinPositions = winPositions;
        }

        /// <summary>
        /// gameBoard string array
        /// </summary>
        [JsonProperty(PropertyName = "gameBoard")]
        public IList<string> GameBoard { get; set; }

        /// <summary>
        /// move integer
        /// </summary>
        [JsonProperty(PropertyName = "move")]
        public int? Move { get; set; }

        /// <summary>
        /// azurePlayerSymbol string
        /// </summary>
        [JsonProperty(PropertyName = "azurePlayerSymbol")]
        public string AzurePlayerSymbol { get; set; }

        /// <summary>
        /// humanPlayerSymbol string
        /// </summary>
        [JsonProperty(PropertyName = "humanPlayerSymbol")]
        public string HumanPlayerSymbol { get; set; }

        /// <summary>
        /// winner string
        /// </summary>
        [JsonProperty(PropertyName = "winner")]
        public string Winner { get; set; }

        /// <summary>
        /// winPositions array or null
        /// </summary>
        [JsonProperty(PropertyName = "winPositions")]
        public IList<int?> WinPositions { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (GameBoard == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "GameBoard");
            }
            if (AzurePlayerSymbol == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AzurePlayerSymbol");
            }
            if (HumanPlayerSymbol == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "HumanPlayerSymbol");
            }
            if (this.GameBoard != null)
            {
                if (this.GameBoard.Count > 9)
                {
                    throw new ValidationException(ValidationRules.MaxItems, "GameBoard", 9);
                }
                if (this.GameBoard.Count < 9)
                {
                    throw new ValidationException(ValidationRules.MinItems, "GameBoard", 9);
                }
            }
            if (this.Move > 8)
            {
                throw new ValidationException(ValidationRules.InclusiveMaximum, "Move", 8);
            }
            if (this.Move < 0)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "Move", 0);
            }
            if (this.AzurePlayerSymbol != null)
            {
                if (this.AzurePlayerSymbol.Length > 1)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "AzurePlayerSymbol", 1);
                }
                if (this.AzurePlayerSymbol.Length < 1)
                {
                    throw new ValidationException(ValidationRules.MinLength, "AzurePlayerSymbol", 1);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.AzurePlayerSymbol, "^[O,X]{1}"))
                {
                    throw new ValidationException(ValidationRules.Pattern, "AzurePlayerSymbol", "^[O,X]{1}");
                }
            }
            if (this.HumanPlayerSymbol != null)
            {
                if (this.HumanPlayerSymbol.Length > 1)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "HumanPlayerSymbol", 1);
                }
                if (this.HumanPlayerSymbol.Length < 1)
                {
                    throw new ValidationException(ValidationRules.MinLength, "HumanPlayerSymbol", 1);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.HumanPlayerSymbol, "^[O,X]{1}"))
                {
                    throw new ValidationException(ValidationRules.Pattern, "HumanPlayerSymbol", "^[O,X]{1}");
                }
            }
        }
    }
}
