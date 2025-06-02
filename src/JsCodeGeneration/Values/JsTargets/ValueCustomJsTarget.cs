using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values.JsTargets;

internal record class ValueCustomJsTarget(CustomJsValueTarget AssociatedCustomJsTarget)
    : ValueJsTarget(AssociatedCustomJsTarget);

