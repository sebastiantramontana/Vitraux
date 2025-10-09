using System.Text;

namespace Vitraux.JsCodeGeneration.Formating;
internal static class StringBuilderExt
{
    public static StringBuilder TryAppendLineForReadability(this StringBuilder sb) 
        => sb.AppendLine().AppendLine();

    public static StringBuilder Add(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => func.Invoke(sb).AppendLine();

    public static StringBuilder Add<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => func.Invoke(sb, arg1).AppendLine();

    public static StringBuilder Add<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => func.Invoke(sb, arg1, arg2).AppendLine();

    public static StringBuilder Add<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => func.Invoke(sb, arg1, arg2, arg3).AppendLine();

    public static StringBuilder Add<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => func.Invoke(sb, arg1, arg2, arg3, arg4).AppendLine();

    public static StringBuilder RemoveLastCharacter(this StringBuilder stringBuilder, char character)
    {
        for (var i = stringBuilder.Length - 1; i >= 0; i--)
        {
            if (stringBuilder[i] == character)
                return stringBuilder.Remove(i, 1);
        }

        return stringBuilder;
    }
}
