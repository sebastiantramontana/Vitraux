namespace Vitraux.JsCodeGeneration.Formating;

internal class CodeFormatter : ICodeFormatter
{
    private const int IndentSpaceCharCount = 4;

    public string Indent(string code)
        => Indent(code, 1);

    public string Indent(string code, int count)
        => Indent(code, new string(' ', IndentSpaceCharCount * count));

    private static string Indent(string code, string indentString)
        => $"{indentString}{ReindentCurrentCodeLines(code, indentString)}".TrimEnd();

    private static string ReindentCurrentCodeLines(string code, string indent)
        => code
            .Split(Environment.NewLine)
            .Aggregate((accumulatedLine, nextLine) => ConcatLines(accumulatedLine, nextLine, indent));

    private static string ConcatLines(string accumulatedLine, string nextLine, string indent)
    {
        var reindentedNextLine = ReindentLineByValidChars(nextLine, indent);
        return $"{accumulatedLine}{Environment.NewLine}{reindentedNextLine}";
    }

    private static string ReindentLineByValidChars(string line, string indent)
        => string.IsNullOrWhiteSpace(line) ? string.Empty : $"{indent}{line}";
}
