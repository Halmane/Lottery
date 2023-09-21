﻿using System.Collections;
using System.Collections.Generic;

namespace Bingo;

public class Board
{
    public int[][] Card { get; private set; }
    public bool[][] IsMatched { get; private set; }
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
        Card = new int[Row][];
        IsMatched = new bool[Row][];
        for (int i = 0; i < Row; i++)
        {
            Card[i] = new int[Column];
            IsMatched[i] = new bool[Column];
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
                Card[i][randomColumn[j]] = number;
            }
        }
    }

    public void WriteCard()
    {
        for (int i = 0; i < Row; i++)
        {
            Console.WriteLine(new string('▬', 5 * Column + 1));
            Console.Write('│');
            var row = Card[i];
            for (int j = 0; j < Column; j++)
            {
                var column = row[j];
                if (column == 0)
                {
                    Console.Write($" {' ', 2} │");
                }
                else if (IsMatched[i][j])
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
        IsMatched[i][j] = true;
    }

    public void IsWin()
    {
        Win = true;
    }
}
