using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Vitraux.Execution.Serialization;

internal class SerializablePropertyValueExtractor : ISerializablePropertyValueExtractor
{
    private readonly Type StringType = typeof(string);

    public SerializableValueInfo GetValueInfo(Delegate @delegate, object? obj)
    {
        var value = @delegate.DynamicInvoke(obj);
        var valueType = @delegate.Method.ReturnType;

        return new(value, valueType, IsSimpleType(valueType));
    }

    public object GetSafeValue(object? value)
        => value ?? string.Empty;

    public IEnumerable<object> GetCollection(Delegate @delegate, object? obj)
        => obj is not null
            ? @delegate.DynamicInvoke(obj) as IEnumerable<object> ?? []
            : [];

    private bool IsSimpleType(Type objType)
    {
        objType = Nullable.GetUnderlyingType(objType) ?? objType;
        return objType.IsPrimitive
            || objType.IsEnum
            || (TypeDescriptor.GetConverter(objType).CanConvertTo(StringType)
                && !IsAutoStringType(objType));
    }

    private static bool IsAutoStringType(Type objType)
    {
        const string ToString = "ToString";
        var toStringMethod = objType.GetMethod(ToString, BindingFlags.Instance | BindingFlags.Public, []);

        return toStringMethod is not null
            && toStringMethod.GetCustomAttribute<CompilerGeneratedAttribute>() is not null;
    }
}
