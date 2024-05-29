using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal interface IElementNamesGenerator
{
    IEnumerable<ElementObjectName> Generate(IEnumerable<ElementSelectorBase> selectors);
}

