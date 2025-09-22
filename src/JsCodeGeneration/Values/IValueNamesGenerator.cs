using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueNamesGenerator
{
    IEnumerable<FullValueObjectName> Generate(IEnumerable<ValueData> values, IEnumerable<JsElementObjectName> currentElementJsObjectNames);
}
