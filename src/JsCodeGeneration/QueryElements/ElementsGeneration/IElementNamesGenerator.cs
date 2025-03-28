using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal interface IElementNamesGenerator
{
    IEnumerable<ElementObjectName> Generate(string namePrefix, IEnumerable<ElementSelectorBase> selectors);
}

