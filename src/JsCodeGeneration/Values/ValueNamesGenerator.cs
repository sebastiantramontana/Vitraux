using Vitraux.Helpers;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueNamesGenerator : IValueNamesGenerator
{
    public IEnumerable<ValueObjectName> Generate(IEnumerable<ValueData> values)
        => values
            .Select((value, indexAsPostfix) => new ValueObjectName($"value{indexAsPostfix}", value))
            .RunNow();
}
