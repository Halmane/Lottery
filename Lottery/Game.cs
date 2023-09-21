using System;

namespace Bingo;

public class Game : IObservable
{
    private Card _playerOne = new Card();
    private Card _playerTwo = new Card();
    private List<int> _bingoBalls = new List<int>();
    private List<int> _bingoBallsList = Enumerable
        .Range(1, 90)
        .OrderBy(x => Random.Shared.Next())
        .ToList();
    List<IObserver> observers;

    public Game()
    {
        while (!IsGameEnd())
        {
            Console.Clear();
            TakeNewBingoBalls();
            WriteBingoBalls();
            IsMatch();
            WriteCards();
        }
    }

    private void WriteCards()
    {
        _playerOne.WriteCard();
        _playerTwo.WriteCard();
    }

    private void IsMatch()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++) 
            {
                if( _bingoBalls.Last() == _playerOne.Board[i][j] )
                    _playerOne.Match(i, j);
                if (_bingoBalls.Last() == _playerTwo.Board[i][j])
                    _playerTwo.Match(i, j);
            }
        }
    }

    private void WriteBingoBalls()
    {
        for (int i = 0; i < _bingoBalls.Count; i++)
        {
            Console.Write($"{_bingoBalls[i]},");
        }
        Console.WriteLine();
    }

    private void TakeNewBingoBalls()
    {
        var number = _bingoBallsList.First();
        _bingoBalls.Add(number);
        NotifyObservers();
        _bingoBallsList.Remove(number);
    }

    private bool IsGameEnd()
    {
        for (int i = 0; i < 3; i++)
        {
            int matchedCountForLinePlayerOne = 0;
            int matchedCountForLinePlayerTwo = 0;
            for (int j = 0; j < 9; j++)
            {
                if (_playerOne.IsMatched[i][j])
                    matchedCountForLinePlayerOne++;
                if (_playerTwo.IsMatched[i][j])
                    matchedCountForLinePlayerTwo++;
            }
            if (matchedCountForLinePlayerOne == 5)
            {
                Console.WriteLine("Первый игрок выиграл!");
                return true;
            }
            else if (matchedCountForLinePlayerTwo == 5)
            {
                Console.WriteLine("Второй игрок выиграл!");
                return true;
            }
        }
        return false;
    }

    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers()
    {
        foreach (var o in observers)
        {
            o.Update(_bingoBalls.Last());
        }
    }
}
