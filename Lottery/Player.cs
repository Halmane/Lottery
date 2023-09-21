using System.Data.Common;

namespace Bingo;

public class Player : IObserver
{
    public List<Board> Boards { get; private set; } = new List<Board>();

    public int PlayerNumber { get; private set; }

    public Player(int number, int boardRow = 3, int boadColumn = 9, int boardCount = 1)
    {
        PlayerNumber = number;
        for (int i = 0; i < boardCount; i++)
        {
            Boards.Add(new Board(i, boardRow, boadColumn));
        }
    }

    public void Update(int bingoBalls)
    {
        foreach (var board in Boards) 
        {
            int numbersCount = board.Column - Board.CountEmptyСells;
            if (numbersCount < 1)
                numbersCount = 1;
            else if (numbersCount * board.Row > 90)
                numbersCount = 90 / board.Row;
            for (int i = 0; i < board.Row; i++)
            {
                var matchCount = 0;
                for (int j = 0; j < board.Column; j++)
                {
                    if (bingoBalls == board.Card[i][j])
                    {
                        board.Match(i, j);
                        Console.WriteLine(
                            $"Player {PlayerNumber} with board number {board.BoardNumber} have the number!"
                        );
                    }
                    if (board.IsMatched[i][j] == true)
                        matchCount++;
                }
                if (matchCount == numbersCount)
                    board.IsWin();
            }
        }
    }
}
