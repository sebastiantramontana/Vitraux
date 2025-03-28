using Vitraux.Modeling.Data.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IJsQueryPopulatingElementsDeclaringGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelectorBase selector);
}