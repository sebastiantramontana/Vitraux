namespace Vitraux.JsCodeGeneration;

internal class CodeFormatting : ICodeFormatting
{
    private const int NumberOfSpacesForIndentation = 4;

    public string Indent(string code)
        => Indent(code, 1);

    public string Indent(string code, int amountOfIndentation)
    {
        const char WhiteSpace = ' ';

        var indent = new String(WhiteSpace, NumberOfSpacesForIndentation * amountOfIndentation);
        return indent + code;
    }
}
