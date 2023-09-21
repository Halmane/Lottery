namespace Bingo;

public static class EnumerableExtensions
{
    public static Queue<T> ToQueue<T>(this IEnumerable<T> e)
    {
        return new Queue<T>(e);
    }
}
