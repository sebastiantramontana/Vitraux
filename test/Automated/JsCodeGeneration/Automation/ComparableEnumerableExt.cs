namespace Vitraux.Test.JsCodeGeneration.Automation;

internal static class ComparableEnumerableExt
{
    internal static ComparableEnumerable<T> ToComparableEnumerable<T>(this IEnumerable<T> source)
        => new(source);
}
