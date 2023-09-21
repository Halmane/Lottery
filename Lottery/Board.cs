using System.Collections;
using System.Collections.Generic;

namespace Bingo;

public class Board
{
    public Cell[][] Cells { get; private set; }
    public bool Win { get; private set; }
    public int BoardNumber { get; private set; }
    public int Column { get; private set; }
    public int Row { get; private set; }
    public const int CountEmptyСells = 4;

    public Board(int boardNumber, int row, int column)
    {
        BoardNumber = boardNumber;
        Column = column;
        Row = row;
        CreateCard();
        FillLines();
    }

    private void CreateCard()
    {
        Cells = new Cell[Row][];
        for (int i = 0; i < Row; i++)
        {
            Cells[i] = new Cell[Column];
            for(int j = 0; j < Column; j++)
            {
                Cells[i][j] = new Cell();
            }
        }
    }

    private void FillLines()
    {
        int numbersCount = Column - CountEmptyСells;
        if (numbersCount < 1)
            numbersCount = 1;
        else if (numbersCount * Row > 90)
            numbersCount = 90 / Row;
        var set = BingoBalls.TakeBingoBalls().Take(numbersCount * Row).ToQueue();
        for (int i = 0; i < Row; i++)
        {
            var randomColumn = Enumerable
                .Range(0, Column)
                .OrderBy(x => Random.Shared.Next())
                .ToList();
            var count = 0;
            for (int j = 0; j < numbersCount; j++)
            {
                var number = set.Dequeue();
                Cells[i][randomColumn[j]].Value = number;
            }
        }
    }

    public void WriteCard()
    {
        for (int i = 0; i < Row; i++)
        {
            Console.WriteLine(new string('▬', 5 * Column + 1));
            Console.Write('│');
            var row = Cells[i];
            for (int j = 0; j < Column; j++)
            {
                var column = row[j].Value;
                if (column == 0)
                {
                    Console.Write($" {' ', 2} │");
                }
                else if (Cells[i][j].State)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {column, 2}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" │");
                }
                else
                    Console.Write($" {column, 2} │");
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('▬', 5 * Column + 1));
    }

    public void Match(int i, int j)
    {
        Cells[i][j].State = true;
    }

    public void IsWin()
    {
        Win = true;
    }
}
