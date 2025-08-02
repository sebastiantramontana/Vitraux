using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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

    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "It is very unlikely")]
    [UnconditionalSuppressMessage("Trimming", "IL2067:Target parameter argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The parameter of method does not have matching annotations.", Justification = "<pendiente>")]
    private bool IsSimpleType(Type objType)
    {
        objType = Nullable.GetUnderlyingType(objType) ?? objType;
        return objType.IsPrimitive
            || objType.IsEnum
            || (TypeDescriptor.GetConverter(objType).CanConvertTo(StringType)
                && !IsAutoStringType(objType));
    }

    [UnconditionalSuppressMessage("Trimming", "IL2070:'this' argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The parameter of method does not have matching annotations.", Justification = "It is very unlikely that the compiler trims the ToString method.")]
    private static bool IsAutoStringType(Type objType)
    {
        const string ToString = "ToString";
        var toStringMethod = objType.GetMethod(ToString, BindingFlags.Instance | BindingFlags.Public, []);

        return toStringMethod is not null
            && toStringMethod.GetCustomAttribute<CompilerGeneratedAttribute>() is not null;
    }
}
