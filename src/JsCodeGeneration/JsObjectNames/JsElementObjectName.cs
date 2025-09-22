using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal record class JsElementObjectName(string Name, SelectorBase AssociatedSelector);