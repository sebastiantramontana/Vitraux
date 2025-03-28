using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IJsQueryPopulatingElementsDeclaringGeneratorContext
{
    IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelectorBase selector);
}