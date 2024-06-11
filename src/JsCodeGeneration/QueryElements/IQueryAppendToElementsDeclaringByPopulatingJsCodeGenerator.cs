using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator
    {
        string GenerateAppendToJsCode(string appendToObjectName, PopulatingAppendToElementSelectorBase elementToAppend, IJsQueryPopulatingElementsDeclaringGeneratorContext jsGeneratorContext);
    }
}