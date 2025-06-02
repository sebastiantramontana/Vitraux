using Vitraux.JsCodeGeneration.Values.JsTargets;

namespace Vitraux.JsCodeGeneration.Values;

internal abstract record class ValueObjectNameWithJsTargets(string Name, IEnumerable<ValueJsTarget> JsTargets);

