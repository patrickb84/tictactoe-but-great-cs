using System;
namespace A03TicTacToe
{
  class Program
  {
    const int topPadding = 3;
    const int leftPadding = 10;
    const int linesInTitle = 4;
    const int topOfBoard = topPadding + linesInTitle - 5; // for convenience in drawing O’s and X’s, 5 accounts for playerPrompt()

    //Draw the gameboard, right now these all imply a square shape for everything
    const int fieldSize = 5; // the height/width of a cell in the grid
    const int lineSize = 1; // dimension of the line drawn for the grid
    const int boardDimension = (3 * fieldSize) + (2 * lineSize);

    const int promptPositionY = topOfBoard + boardDimension + 6;

    static bool isXsTurn = true; // starts with X right?        
    static int fieldCount = 0; // when this is 8, a the oldest field has to be dropped
    static bool gameWon = false; // TODO 
    static int boardAge = 0; //age of the board
    static GameSquare lastSquare;  //last square a token was placed on

    static GameSquare[] gameBoard = {new GameSquare(), new GameSquare(), new GameSquare(),
                                         new GameSquare(), new GameSquare(), new GameSquare(),
                                         new GameSquare(), new GameSquare(), new GameSquare()};






    public static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      DrawGameBoard();

      do
      {
        Console.WriteLine();        //corrects the placement of the prompt
        playerPrompt(isXsTurn);     //interface

        bool legalMove = false;  //exits the loop
        do
        {
          string input = Console.ReadLine();  // reads in after playerprompt
          legalMove = placeToken(Convert.ToInt32(input), true);  // place O or X where player says, true means it's a player move
          if (legalMove && fieldCount > 6)  //if the move is legit and we have more than 7 things, remove the lowest age thing
          {
            Console.WriteLine("Here we go");
            removeLowestAge();
            Console.WriteLine("Passed");
          }
        }
        while (!legalMove);

      }
      while (!gameWon);
    }

    private static void removeLowestAge()
    {
      int lowestAge = boardAge; // guaranteed oldest
      GameSquare lowestAgeSquare = lastSquare; // guaranteed to be a filled square
      int squareInt = 0;

      for (int i = 0; i < 9; i++) // iterate through
      {
        if (gameBoard[i].IsFilled) // only check the filled squares/ignore empty squares
        {
          if (gameBoard[i].Age < lowestAge) // find lowest age
          {
            lowestAge = gameBoard[i].Age; // update age
            lowestAgeSquare = gameBoard[i]; // find the oldest square to remove
            squareInt = i;
          }
        }
      }
      lowestAgeSquare.clearSquare();
      Console.WriteLine(squareInt);
      placeToken(squareInt, false);
    }

    /*
     * Place's token's (X's or O's)
     */
    public static bool placeToken(int whichSquare, bool isPlayer)
    {
      String token = "X";
      if (!isXsTurn)
        token = "O";

      // positioning
      int center = 2; // though a constant relative to size of square would be more appropriate
      int row = center + topOfBoard + 5; // top position y axis
      int col = center + leftPadding; // left position x axis
      int pos2 = fieldSize + lineSize; // add to get the 2nd position, either axis
      int pos3 = 2 * pos2; // add to get the 3rd position, either axis

      switch (whichSquare)
      {
        case 1:
          //cursor placement
          Console.SetCursorPosition(col, row);

          if (isPlayer)
          {
            //place token if not empty
            if (!gameBoard[0].IsFilled)
            {
              Console.WriteLine(token);           //draws the token
              SuccessfulPlayerMove(0);            //runs player stuff
              return true;                        //breaks do-while loop
            }
          }

          // else means removeLowestAge() is taking place
          else
            Console.WriteLine(" ");                 //removes the token

          return false;                           //stays inside do-while

        case 2:
          Console.SetCursorPosition(col + pos2, row);

          if (isPlayer)
          {
            if (!gameBoard[1].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(1);
              return true;
            }
          }

          else
            Console.WriteLine(" ");

          return false;

        case 3:
          Console.SetCursorPosition(col + pos3, row);

          if (isPlayer)
          {
            if (!gameBoard[2].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(2);
              return true;
            }
          }

          else
            Console.WriteLine(" ");

          return false;

        case 4:
          Console.SetCursorPosition(col, row + pos2);

          if (isPlayer)
          {
            if (!gameBoard[3].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(3);
              return true;
            }
          }

          else
            Console.WriteLine(" ");

          return false;

        case 5:
          Console.SetCursorPosition(col + pos2, row + pos2);

          if (isPlayer)
          {
            if (!gameBoard[4].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(4);
              return true;
            }
          }

          else
            Console.WriteLine(" ");

          return false;

        case 6:
          Console.SetCursorPosition(col + pos3, row + pos2);

          if (isPlayer)
          {
            if (!gameBoard[5].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(5);
              return true;
            }
          }
          else
            Console.WriteLine(" ");

          return false;

        case 7:
          Console.SetCursorPosition(col, row + pos3);

          if (isPlayer)
          {
            if (!gameBoard[6].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(6);
              return true;
            }
          }
          else
            Console.WriteLine(" ");

          return false;
        case 8:
          Console.SetCursorPosition(col + pos2, row + pos3);

          if (isPlayer)
          {
            if (!gameBoard[7].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(7);
              return true;
            }
          }
          else
            Console.WriteLine(" ");

          return false;
        case 9:
          Console.SetCursorPosition(col + pos3, row + pos3);

          if (isPlayer)
          {
            if (!gameBoard[8].IsFilled)
            {
              Console.WriteLine(token);
              SuccessfulPlayerMove(8);
              return true;
            }
          }
          else
            Console.WriteLine(" ");

          return false;
        default:
          Console.WriteLine("Select an empty field from 1 to 9.");
          return false;
      }
    }

    private static void SuccessfulPlayerMove(int square)
    {
      nextPlayer(isXsTurn);               //changes turn
      gameBoard[square].IsFilled = true;       //square is filled
      boardAge++;                         //advances age
      gameBoard[square].Age = boardAge;        //sets age of square to current board age
      Console.WriteLine(boardAge + " " + gameBoard[square].Age); //delete
      fieldCount++;                       //one more square is filled
      lastSquare = gameBoard[square];          //now this is the last square that was filled
    }

    public static void nextPlayer(bool isX)
    {
      if (isX)
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        isXsTurn = false;
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        isXsTurn = true;
      }
    }

    public static void playerPrompt(bool isXsTurn)
    {
      Console.SetCursorPosition(0, promptPositionY);
      //need to account for space taken up by this when marking the grid
      String marker = "X";
      if (!isXsTurn) marker = "O";
      Console.WriteLine("1 | 2 | 3\n4 | 5 | 6\n7 | 8 | 9\n");
      Console.WriteLine(marker + "'s turn, where do you want to place a marker ? (1 - 9)");
    }

    public static void DrawGameBoard()
    {
      //Write the title
      Console.CursorTop = topPadding;
      Console.CursorLeft = leftPadding + 6;
      Console.WriteLine("Tic");
      Console.CursorLeft = leftPadding + 8;
      Console.WriteLine("Tac");
      Console.CursorLeft = leftPadding + 10;
      Console.WriteLine("Toe");
      Console.WriteLine();
      Console.BackgroundColor = ConsoleColor.Blue;
      for (int i = 0; i < boardDimension; i++) // boardlength 17 as of "now"
      {
        Console.CursorLeft = leftPadding + fieldSize;
        Console.Write("{0,1}", "");  // 1 should be boardspace
        Console.CursorLeft = leftPadding + lineSize + (2 * fieldSize);
        Console.Write("{0,1}", "");
        Console.WriteLine();
      }
      for (int i = 0; i < boardDimension - fieldSize; i = i + fieldSize + lineSize)
      {
        Console.CursorTop = topPadding + fieldSize + linesInTitle + i;
        Console.CursorLeft = leftPadding;
        Console.WriteLine("{0,17}", "");  // 17 should be boardlength
      }
      Console.BackgroundColor = ConsoleColor.Black;
    }
  }
}