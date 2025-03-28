using Vitraux.Modeling.Data.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class PopulatingElementObjectName(string Name, string AppendToName, PopulatingElementSelectorBase PopulatingElementSelector)
    : ElementObjectName(Name, PopulatingElementSelector);
