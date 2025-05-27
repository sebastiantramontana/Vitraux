using System;
using Vitraux.Helpers;

namespace Vitraux.JsCodeGeneration;

internal class CodeFormatter : ICodeFormatter
{
    private const int NumberOfSpacesForIndentation = 4;

    public string Indent(string code)
    {
        const char WhiteSpace = ' ';

        var indent = new string(WhiteSpace, NumberOfSpacesForIndentation);
        return $"{indent}{ReindentCurrentCodeLines(code, indent)}".TrimEnd();
    }

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
