namespace Bingo;

public static class BingoBalls
{
    public static IEnumerable<int> TakeBingoBalls()
    {
        return Enumerable.Range(1, 90).OrderBy(x => Random.Shared.Next());
    }
}
