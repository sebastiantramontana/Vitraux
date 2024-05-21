using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueNamesGenerator
{
    IEnumerable<ValueObjectName> Generate(IEnumerable<ValueModel> values);
}
