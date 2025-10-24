using System.Text;

namespace Vitraux.JsCodeGeneration.Formating;
internal static class StringBuilderExt
{
    private static readonly string _twoNewLines = $"{Environment.NewLine}{Environment.NewLine}";

    public static StringBuilder TryAppendTwoLinesForReadability(this StringBuilder sb)
        => sb.TryAppendLinesForReadability(_twoNewLines);

    public static StringBuilder Add(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => func.Invoke(sb);

    public static StringBuilder Add<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => func.Invoke(sb, arg1);

    public static StringBuilder Add<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => func.Invoke(sb, arg1, arg2);

    public static StringBuilder Add<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => func.Invoke(sb, arg1, arg2, arg3);

    public static StringBuilder Add<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => func.Invoke(sb, arg1, arg2, arg3, arg4);

    public static StringBuilder Add<T1, T2, T3, T4, T5>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, T5, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => func.Invoke(sb, arg1, arg2, arg3, arg4, arg5);

    public static StringBuilder AddLine(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => sb.Add(func).AppendLine();

    public static StringBuilder AddLine<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => sb.Add(func, arg1).AppendLine();

    public static StringBuilder AddLine<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => sb.Add(func, arg1, arg2).AppendLine();

    public static StringBuilder AddLine<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => sb.Add(func, arg1, arg2, arg3).AppendLine();

    public static StringBuilder AddLine<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => sb.Add(func, arg1, arg2, arg3, arg4).AppendLine();

    public static StringBuilder AddLine<T1, T2, T3, T4, T5>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, T5, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => sb.Add(func, arg1, arg2, arg3, arg4, arg5).AppendLine();

    public static StringBuilder AddTwoLines(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => sb.Add(func).AppendLine().AppendLine();

    public static StringBuilder AddTwoLines<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => sb.Add(func, arg1).AppendLine().AppendLine();

    public static StringBuilder AddTwoLines<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => sb.Add(func, arg1, arg2).AppendLine().AppendLine();

    public static StringBuilder AddTwoLines<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => sb.Add(func, arg1, arg2, arg3).AppendLine().AppendLine();

    public static StringBuilder AddTwoLines<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => sb.Add(func, arg1, arg2, arg3, arg4).AppendLine().AppendLine();

    public static StringBuilder AddTwoLines<T1, T2, T3, T4, T5>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, T5, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => sb.Add(func, arg1, arg2, arg3, arg4, arg5).AppendLine().AppendLine();

    public static StringBuilder TryAddLine(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => sb.TryAddLines(sb2 => sb2.Add(func), 1);

    public static StringBuilder TryAddLine<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1), 1);

    public static StringBuilder TryAddLine<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2), 1);

    public static StringBuilder TryAddLine<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3), 1);

    public static StringBuilder TryAddLine<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3, arg4), 1);

    public static StringBuilder TryAddLine<T1, T2, T3, T4, T5>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, T5, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3, arg4, arg5), 1);

    public static StringBuilder TryAddTwoLines(this StringBuilder sb, Func<StringBuilder, StringBuilder> func)
        => sb.TryAddLines(sb2 => sb2.Add(func), 2);

    public static StringBuilder TryAddTwoLines<T1>(this StringBuilder sb, Func<StringBuilder, T1, StringBuilder> func, T1 arg1)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1), 2);

    public static StringBuilder TryAddTwoLines<T1, T2>(this StringBuilder sb, Func<StringBuilder, T1, T2, StringBuilder> func, T1 arg1, T2 arg2)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2), 2);

    public static StringBuilder TryAddTwoLines<T1, T2, T3>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3), 2);

    public static StringBuilder TryAddTwoLines<T1, T2, T3, T4>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3, arg4), 2);

    public static StringBuilder TryAddTwoLines<T1, T2, T3, T4, T5>(this StringBuilder sb, Func<StringBuilder, T1, T2, T3, T4, T5, StringBuilder> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        => sb.TryAddLines(sb2 => sb2.Add(func, arg1, arg2, arg3, arg4, arg5), 2);

    public static StringBuilder RemoveLastCharacter(this StringBuilder stringBuilder, char character)
    {
        for (var i = stringBuilder.Length - 1; i >= 0; i--)
        {
            if (stringBuilder[i] == character)
                return stringBuilder.Remove(i, 1);
        }

        return stringBuilder;
    }

    public static StringBuilder TryAddLines(this StringBuilder sb, Func<StringBuilder, StringBuilder> func, int newLinesCount)
    {
        var previousLength = sb.Length;
        var newSb = func.Invoke(sb);

        return newSb.Length > previousLength ? newSb.AppendNewLines(newLinesCount) : newSb;
    }

    public static StringBuilder TrimEnd(this StringBuilder sb)
    {
        var whiteSpacelength = 0;

        for (var i = sb.Length - 1; i >= 0 && char.IsWhiteSpace(sb[i]); i--, whiteSpacelength++) ;

        return sb.Remove(sb.Length - whiteSpacelength, whiteSpacelength);
    }

    private static StringBuilder AppendNewLines(this StringBuilder sb, int newLinesCount)
    {
        for (var i = 0; i < newLinesCount; i++)
            sb = sb.AppendLine();

        return sb;
    }

    private static StringBuilder TryAppendLinesForReadability(this StringBuilder sb, string newLines)
        => sb.IsShorterThanNewLines(newLines) || sb.EndsWithNewLines(newLines)
            ? sb
            : sb.Append(newLines);

    private static bool IsShorterThanNewLines(this StringBuilder sb, string newLines)
        => sb.Length < newLines.Length;

    private static bool EndsWithNewLines(this StringBuilder sb, string newLines)
    {
        for (var i = 0; i < newLines.Length; i++)
        {
            if (sb[^(newLines.Length - i)] != newLines[i])
                return false;
        }

        return true;
    }
}
