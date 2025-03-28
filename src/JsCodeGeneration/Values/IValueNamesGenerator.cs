using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueNamesGenerator
{
    IEnumerable<ValueObjectName> Generate(IEnumerable<ValueData> values);
}
