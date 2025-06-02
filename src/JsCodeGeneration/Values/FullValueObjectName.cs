using Vitraux.JsCodeGeneration.Values.JsTargets;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal record class FullValueObjectName(string Name, IEnumerable<ValueJsTarget> JsTargets, ValueData AssociatedValue)
    : ValueObjectNameWithJsTargets(Name, JsTargets);

