using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values.JsTargets;

internal record class ValueOwnMappingTarget(OwnMappingTarget AssociatedOwnMappingTarget)
    : ValueJsTarget(AssociatedOwnMappingTarget);

