using System.Collections;

namespace Vitraux.Test.JsCodeGeneration.Automation;

internal class ComparableEnumerable<T>(IEnumerable<T> source) : IEnumerable<T>, IEquatable<IEnumerable<T>>
{
    public bool Equals(IEnumerable<T>? other)
        => other is not null && source.SequenceEqual(other);

    public override bool Equals(object? obj)
        => Equals(obj as ComparableEnumerable<T>);

    public IEnumerator<T> GetEnumerator()
        => source.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public override int GetHashCode()
        => source.GetHashCode();
}
