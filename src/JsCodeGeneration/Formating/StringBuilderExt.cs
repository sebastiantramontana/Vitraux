using System.Text;

namespace Vitraux.JsCodeGeneration.Formating;
internal static class StringBuilderExt
{
    public static StringBuilder TryAppendLineForReadability(this StringBuilder sb)
    {
        var str = sb.ToString();

        return !str.EndsWith($"{Environment.NewLine}{Environment.NewLine}")
            ? sb.AppendLine().AppendLine()
            : sb;
    }
}
