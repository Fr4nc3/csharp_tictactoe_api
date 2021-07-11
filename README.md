# fr4nc3.com.tictactoe

open project and build no error or warning

## Rest api

- GameMove model
- GameMoveResponse model
- Validation using Annotations
- Models have internal fields used to validate Model
  Aditional rules
- if gameboard comes full of '?' ignore move variable and Azure play first
- human move integer shall came filled in the gameboard too
  ie: move 1, human =X board = ["O", "X", "O", "X", "?", "X", "X", "O", "O"]
- swagger enabled by class example version 2.0
- rest api has one controller
- tictactoeController
- two method
- executemove
- calulatemove
- RestClientSDKLibrary
- implemented by class example
- I noticed that the annottations created a validation function on GameMove and SelfMove
- I disable these validation
- ie. commented line 438 fr4nc3.com.tictactoe\RestClientSDKLibrary\RestClientSDKLibraryClient\RestClientSDKLibraryClient.cs
- I noticed the Body for 400 error expected on RestClientSDKLibrary was int? I converted to object
  ie. file fr4nc3.com.tictactoe\RestClientSDKLibrary\RestClientSDKLibraryClient\RestClientSDKLibraryClient.cs
  from
  \_result.Body = SafeJsonConvert.DeserializeObject<int>(\_responseContent, this.DeserializationSettings);
  to
  \_result.Body = SafeJsonConvert.DeserializeObject<object>(\_responseContent, this.DeserializationSettings);

### Self play

- I was able to write calculatemove gameboard
- to follow these rules
- take player symbol and random get an available position
- check if there is a winner and return the results
- this version only move one player symbol at the time
- it has the same validation annotation that GameMove
- in the MSTest to play I created a loop that switch between X and O player to request next movement
- from the HW example, I was not clear if after the player symbol moves the other player also moves.
- I mean, It can be two movements per request, but I decided one move per request, and the player need to be changing by request.

### MSTest

- MSTest for calculatemove
- single game
- 100 games in a loop with results
  ie.
  winnerX: 48
  winnerO: 36
  winnerTie: 16

- executemove MStest 20 test for differents constrains and restriction for the game in the code
- calulatemove MStest 8 test for differents constrains and restriction for the game in the code and a self play and 100 time self play

### References

https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio

https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-5.0

https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
