using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal record class ValueObjectNameWithData(string Name, ValueData AssociatedValue);

