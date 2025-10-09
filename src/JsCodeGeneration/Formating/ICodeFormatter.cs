namespace Vitraux.JsCodeGeneration.Formating;

internal interface ICodeFormatter
{
    string Indent(string code);
    string Indent(string code, int count);
}
