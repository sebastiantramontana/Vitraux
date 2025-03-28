using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class PopulatingElementObjectName(string Name, string AppendToName, InsertElementSelectorBase PopulatingElementSelector)
    : ElementObjectName(Name, PopulatingElementSelector);
