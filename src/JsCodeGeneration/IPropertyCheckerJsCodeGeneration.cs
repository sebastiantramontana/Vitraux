namespace Vitraux.JsCodeGeneration;

internal interface IPropertyCheckerJsCodeGeneration
{
    string GenerateJs(string objectParentName, string valueObjectName, string jsCodeBlock);
}
