using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueNamesGenerator : IValueNamesGenerator
{
    public IEnumerable<ValueObjectName> Generate(IEnumerable<ValueModel> values)
        => values.Select((value, indexAsPostfix) => new ValueObjectName($"value{indexAsPostfix}", value));
}
