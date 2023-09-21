using System.Data.Common;

namespace Bingo;

public class Player : IObserver
{
    public List<Board> Boards { get; private set; } = new List<Board>();

    public int PlayerNumber { get; private set; }

    public Player(int number, int boardRow = 3, int boadColumn = 9, int boardCount = 1)
    {
        PlayerNumber = number;
        for(int i = 0; i < boardCount; i++) 
        {
            Boards.Add(new Board(i, boardRow, boadColumn));
        }
    }

    public void Update(int bingoBalls)
    {

        for (int k = 0; k < Boards.Count; k++)
        {
            int numbersCount = Boards[k].Column - Board.CountEmptyСells;
            if (numbersCount < 1) numbersCount = 1;
            else if (numbersCount * Boards[k].Row > 90) numbersCount = 90 / Boards[k].Row;
            for (int i = 0; i < Boards[k].Row; i++)
            {
                var matchCount = 0;
                for (int j = 0; j < Boards[k].Column; j++)
                {
                    if ((int)bingoBalls == Boards[k].Card[i][j])
                    {
                        Boards[k].Match(i, j);
                        Console.WriteLine(
                            $"Player {PlayerNumber} with board number {Boards[k].BoardNumber} have the number!");
                    }
                    if (Boards[k].IsMatched[i][j] == true)
                        matchCount++;
                }
                if (matchCount == numbersCount)
                    Boards[k].IsWin();
            }
        }
    }
}
