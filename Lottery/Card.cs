namespace Bingo;

public class Card : IObserver
{
    public int[][] Board { get; private set; } = { new int[9], new int[9], new int[9] };
    public bool[][] IsMatched { get; private set; } = { new bool[9], new bool[9], new bool[9] };
    private IObservable _game;
    public Card(IObservable game)
    {
        FillLines();
        _game = game;
        _game.RegisterObserver(this);
    }

    private void FillLines()
    {
        var set = Enumerable.Range(1, 90).OrderBy(x => Random.Shared.Next()).Take(15).ToList();
        for (int i = 0; i < 3; i++)
        {
            FillLine(Board[i], set);
            
        }
    }

    private void FillLine(int[] line, List<int> set)
    {
        var randomColumn = Enumerable.Range(0, 9).OrderBy(x => Random.Shared.Next()).ToList();
        var count = 0;
        for (int i = 0; i < 5; i++)
        {
            var number = set.First();
            line[randomColumn[i]] = number;
            set.Remove(number);
        }
    }

    public void WriteCard()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            Console.WriteLine(new string('▬', 5 * 9 + 1));
            Console.Write('│');
            for (int j = 0; j < Board[i].Length; j++)
            {
                if (Board[i][j] == 0)
                {

                    Console.Write($" {' ',2} │");
                }
                else if (IsMatched[i][j])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {Board[i][j],2}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" │");
                }
                else
                    Console.Write($" {Board[i][j],2} │");
                }
            Console.WriteLine();
        }
        Console.WriteLine(new string('▬', 5 * 9 + 1));
    }

    public void Match(int i, int j)
    {
        IsMatched[i][j] = true;
    }

    public void Update(object bingoBalls)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if ((int)bingoBalls == Board[i][j])
                    Match(i, j);
            }
        }
    }
}
