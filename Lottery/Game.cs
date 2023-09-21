using System;

namespace Bingo;

public class Game : IObservable
{
    private List<int> _bingoBalls = new List<int>();
    private List<int> _bingoBallsList = Enumerable
        .Range(1, 90)
        .OrderBy(x => Random.Shared.Next())
        .ToList();
    List<IObserver> observers;

    public Game()
    {
        observers = new List<IObserver>();
    }

    public void Play()
    {
        Console.Clear();
        TakeNewBingoBalls();
        WriteBingoBalls();
        Thread.Sleep(500);
        NotifyObservers();
        Thread.Sleep(500);
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
