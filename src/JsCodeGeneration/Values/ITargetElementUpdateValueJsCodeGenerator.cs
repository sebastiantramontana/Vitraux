using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementUpdateValueJsCodeGenerator
{
    string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string parentValueObjectName, string valueObjectName);
}