using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values.JsTargets;

internal record class ValueElementTargetJsObjectName(ElementValueTarget AssociatedElementTarget, JsObjectName JsElementObjectName, JsObjectName? JsInsertionElementObjectName)
    : ValueJsTarget(AssociatedElementTarget);

