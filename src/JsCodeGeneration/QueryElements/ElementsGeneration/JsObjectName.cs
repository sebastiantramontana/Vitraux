using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class JsObjectName(string Name, SelectorBase AssociatedSelector);