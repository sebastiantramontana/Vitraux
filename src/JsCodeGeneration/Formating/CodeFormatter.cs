namespace Vitraux.JsCodeGeneration.Formating;

internal class CodeFormatter : ICodeFormatter
{
    private const int IndentSpaceCharCount = 4;

    public string IndentLine(string lineOfCode, int count)
        => IndentLine(lineOfCode, new string(' ', IndentSpaceCharCount * count));

    private static string IndentLine(string lineOfCode, string indentString)
        => !string.IsNullOrWhiteSpace(lineOfCode)
            ? $"{indentString}{lineOfCode}"
            : string.Empty;
}
