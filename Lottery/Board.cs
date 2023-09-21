namespace Bingo;

public class Board
{
    public List<List<(int number, bool isMatched)>> Card { get; private set;} = new List<List<(int number, bool isMatched)>>();
    public bool Win { get; private set; }
    public int BoardNumber { get; private set; }
    public int Column { get; private set; }
    public int Row { get; private set; }

    public Board(int boardNumber,int row, int column)
    {
        BoardNumber = boardNumber;
        Column = column;
        Row = row;
        CreateCard();
        FillLines();
    }

    private void CreateCard()
    {
        for (int i = 0; i < Row; i++) 
        {
            Card.Add(new List<(int number, bool isMatched)>());
            for (int j = 0; j < Column; j++)
            {
                Card[i].Add((0, false));
            }
        }
    }
    private void FillLines()
    {
        int numbersCount = Column - 4;
        if(numbersCount < 1) numbersCount = 1;
        else if(numbersCount * Row > 90) numbersCount = 90/Row;
        var set = Enumerable.Range(1, 90).OrderBy(x => Random.Shared.Next()).Take(numbersCount * Row).ToList();
        for (int i = 0; i < Row; i++)
        {
            var randomColumn = Enumerable.Range(0, Column).OrderBy(x => Random.Shared.Next()).ToList();
            var count = 0;
            for (int j = 0; j < numbersCount; j++)
            {
                var number = set.First();
                Card[i][randomColumn[j]] = (number,false);
                set.Remove(number);
            }
        }
    }

    public void WriteCard()
    {
        for (int i = 0; i < Row; i++)
        {
            Console.WriteLine(new string('▬', 5 * Column + 1));
            Console.Write('│');
            for (int j = 0; j < Column; j++)
            {
                if (Card[i][j].number == 0)
                {

                    Console.Write($" {' ',2} │");
                }
                else if (Card[i][j].isMatched)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {Card[i][j].number,2}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" │");
                }
                else
                    Console.Write($" {Card[i][j].number,2} │");
            }
            Console.WriteLine();
        }
        Console.WriteLine(new string('▬', 5 * Column + 1));
    }

    public void Match(int i, int j)
    {
        Card[i][j] = (Card[i][j].number, true);
    }

    public void IsWin()
    {
        Win = true;
    }
}
