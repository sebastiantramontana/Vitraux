using FluentAssertions;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Vitraux.Test.Modeling.Building;

public static class VitrauxDataSerializer
{
    public static string Serialize(object? obj, int indentLevel)
    {
        const int indentSpace = 2;

        if (obj is null) return "null";

        var currentIndent = new string(' ', indentLevel + indentSpace);
        var bracketsIndent = new string(' ', indentLevel);

        var sb = new StringBuilder();
        var type = obj.GetType();

        if (IsVitrauxType(type))
        {
            sb.AppendLine($"{bracketsIndent}{{");

            foreach (var prop in GetFilteredProperties(type))
            {
                var value = prop.GetValue(obj);
                var propType = value?.GetType() ?? prop.PropertyType;

                sb.Append($"{currentIndent}({propType.Name}) {prop.Name}: ");

                if (IsIEnumerableOfVitrauxType(propType))
                {
                    sb.AppendLine("[");

                    var enumerable = (value as IEnumerable)!;

                    foreach (var item in enumerable)
                    {
                        sb.AppendLine(Serialize(item, indentLevel + indentSpace));
                    }

                    sb.Append($"{bracketsIndent}]");
                }
                else
                {
                    sb.AppendLine(Serialize(value, indentLevel + indentSpace));
                }
            }

            sb.Append($"{bracketsIndent}}}");
        }
        else
        {
            sb.AppendLine($"({type.Name}) {obj}");
        }

        return sb.ToString();
    }

    private static IEnumerable<PropertyInfo> GetFilteredProperties(Type type)
    {
        IEnumerable<string> ignoredProperties = ["EqualityContract", "FunctionPointerReturnAndParameterTypes"];

        return type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
            .Where(p => !ignoredProperties.Contains(p.Name));
    }

    private static bool IsIEnumerableOfVitrauxType(Type type)
    {
        if (!type.IsAssignableTo(typeof(IEnumerable)))
            return false;

        var generics = type.GetGenericArguments();

        if (generics.Length != 1)
            return false;

        return IsVitrauxType(generics[0]);
    }

    private static bool IsVitrauxType(Type type)
        => type.Namespace?.Contains("Vitraux") ?? false;
}
