using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator
    {
        string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelectorBase elementToAppend, IJsQueryPopulatingElementsDeclaringGeneratorContext jsGeneratorContext);
    }
}