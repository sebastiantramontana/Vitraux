namespace Vitraux.JsCodeGeneration.Formating;

internal interface ICodeFormatter
{
    string IndentLine(string lineOfCode, int count);
}
