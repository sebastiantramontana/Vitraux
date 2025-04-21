using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementUpdateValueJsCodeGenerator
{
    string GenerateJsCode(ElementValueTarget targetElement, IEnumerable<ElementObjectName> associatedElements, string parentValueObjectName, string valueObjectName);
}