namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueCheckJsCodeGeneration
{
    string GenerateJsCode(string valueObjectName, string jsCodeBlock);
}
