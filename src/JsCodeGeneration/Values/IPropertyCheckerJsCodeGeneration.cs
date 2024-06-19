namespace Vitraux.JsCodeGeneration.Values;

internal interface IPropertyCheckerJsCodeGeneration
{
    string GenerateJsCode(string objectParentName, string valueObjectName, string jsCodeBlock);
}
