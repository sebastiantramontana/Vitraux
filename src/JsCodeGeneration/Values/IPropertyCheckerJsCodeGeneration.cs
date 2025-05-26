namespace Vitraux.JsCodeGeneration.Values;

internal interface IPropertyCheckerJsCodeGeneration
{
    string GenerateJs(string objectParentName, string valueObjectName, string jsCodeBlock);
}
