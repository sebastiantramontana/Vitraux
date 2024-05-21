namespace Vitraux.JsCodeGeneration;

internal interface ICodeFormatting
{
    string Indent(string code);
    string Indent(string code, int amountOfIndentation);
}
