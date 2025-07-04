using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface IValueNamesGenerator
{
    IEnumerable<FullValueObjectName> Generate(IEnumerable<ValueData> values, IEnumerable<JsObjectName> currentElementJsObjectNames);
}
