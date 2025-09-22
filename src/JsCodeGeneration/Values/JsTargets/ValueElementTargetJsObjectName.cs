using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values.JsTargets;

internal record class ValueElementTargetJsObjectName(ElementValueTarget AssociatedElementTarget, JsElementObjectName JsElementObjectName, JsElementObjectName? JsInsertionElementObjectName)
    : ValueJsTarget(AssociatedElementTarget);

