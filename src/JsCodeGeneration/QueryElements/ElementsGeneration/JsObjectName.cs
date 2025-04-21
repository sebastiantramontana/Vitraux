using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class JsObjectName(string JsObjName, SelectorBase AssociatedSelector);