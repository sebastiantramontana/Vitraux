using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
    {
        string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelector elementToAppend, IJsQueryFromTemplateElementsDeclaringGeneratorFactory jsGeneratorFactory);
    }
}