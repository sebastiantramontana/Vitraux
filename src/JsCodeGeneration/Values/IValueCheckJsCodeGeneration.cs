namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueCheckJsCodeGeneration
{
    string GenerateJsCode(string objectParentName, string valueObjectName, string jsCodeBlock);
}
