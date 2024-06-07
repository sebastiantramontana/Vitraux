using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator
{
    string GenerateJsCode(ElementObjectName elementObjectName, Func<string> getElementTemplateCallingFunc, IJsQueryPopulatingElementsDeclaringGeneratorContext queryGeneratorContext);
}
