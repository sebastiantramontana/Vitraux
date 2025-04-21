namespace Vitraux.Helpers;

internal static class LinqExtensions
{
    public static IEnumerable<T> RunNow<T>(this IEnumerable<T> source) => [.. source];
}
