using System;

namespace Bingo;

public class Game : IObservable
{
    private readonly List<int> _bingoBalls = new List<int>();
    private readonly Queue<int> _bingoBallsList = BingoBalls.TakeBingoBalls().ToQueue();
    private readonly List<IObserver> _observers;

    public Game()
    {
        _observers = new List<IObserver>();
    }

    private void WriteBingoBalls()
    {
        foreach(var bingoball  in _bingoBalls) 
        {
            Console.Write($"{bingoball},");
        }
        Console.WriteLine();
    }

    private void TakeNewBingoBalls()
    {
        var number = _bingoBallsList.Dequeue();
        _bingoBalls.Add(number);
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
