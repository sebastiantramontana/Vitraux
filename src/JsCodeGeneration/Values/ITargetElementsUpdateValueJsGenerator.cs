using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsUpdateValueJsGenerator
{
    string GenerateJsCode(ElementValueTarget targetElement, IEnumerable<JsObjectName> associatedObjects, string parentValueObjectName, string valueObjectName);
}