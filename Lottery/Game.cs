using System;

namespace Bingo;

public class Game : IObservable
{
    private readonly List<int> _bingoBalls = new List<int>();
    private readonly List<int> _bingoBallsList = Enumerable
        .Range(1, 90)
        .OrderBy(x => Random.Shared.Next())
        .ToList();
    private readonly List<IObserver> _observers;

    public Game()
    {
        _observers = new List<IObserver>();
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
        _bingoBallsList.Remove(number);
    }

    public void Play()
    {
        Console.Clear();
        TakeNewBingoBalls();
        WriteBingoBalls();
        //Thread.Sleep(500);
        NotifyObservers();
        //Thread.Sleep(500);
    }

    public void RegisterObserver(IObserver o)
    {
        _observers.Add(o);
    }

    public void RemoveObserver(IObserver o)
    {
        _observers.Remove(o);
    }

    public void NotifyObservers()
    {
        foreach (var o in _observers)
        {
            o.Update(_bingoBalls.Last());
        }
    }
}
