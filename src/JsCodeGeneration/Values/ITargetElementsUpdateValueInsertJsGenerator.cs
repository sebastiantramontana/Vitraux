using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsUpdateValueInsertJsGenerator
{
    string GenerateJs(ElementValueTarget targetElement, JsObjectName elementToInsertObjectName, JsObjectName elementsToAppendObjectName, string parentValueObjectName, string valueObjectName);
}