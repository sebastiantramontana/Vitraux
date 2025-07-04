using System.Reflection;
using System.Runtime.CompilerServices;

namespace Vitraux.Execution.Serialization;

internal class SerializablePropertyValueExtractor : ISerializablePropertyValueExtractor
{
    private const string NonJsFalsyEmptyStringValue = "";

    public string GetValue(Delegate @delegate, object? obj)
    {
        var value = @delegate?.DynamicInvoke(obj);
        return GetStringValue(value);
    }

    public IEnumerable<object> GetCollection(Delegate @delegate, object? value)
        => value is not null
            ? @delegate.DynamicInvoke(value) as IEnumerable<object> ?? []
            : [];

    private static string GetStringValue(object? value)
    {
        if (value is null)
            return NonJsFalsyEmptyStringValue;

        var toStringMethod = value.ToString;

        return IsValidTypeForToString(toStringMethod.Method)
            ? value.ToString() ?? NonJsFalsyEmptyStringValue
            : NonJsFalsyEmptyStringValue;
    }

    private static bool IsValidTypeForToString(MethodInfo toStringMethod)
        => toStringMethod.DeclaringType!.IsPrimitive
        || (toStringMethod.DeclaringType != typeof(object)
            && toStringMethod.GetCustomAttribute<CompilerGeneratedAttribute>() is null);
}
