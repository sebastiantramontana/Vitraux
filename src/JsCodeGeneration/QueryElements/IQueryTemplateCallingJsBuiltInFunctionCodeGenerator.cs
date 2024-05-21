using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryTemplateCallingJsBuiltInFunctionCodeGenerator
{
    string GenerateJsCode(ElementObjectName elementObjectName, Func<string> getElementTemplateCallingFunc, IJsQueryFromTemplateElementsDeclaringGeneratorFactory queryGeneratorFactory);
}
