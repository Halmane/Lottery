namespace Bingo;

public static class QueueExtension
{
    public static Queue<T> ToQueue<T>(this IEnumerable<T> e)
    {
        var q = new Queue<T>(e);
        return q;
    }
}
