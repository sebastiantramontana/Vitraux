using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsUpdateValueInsertJsGenerator
{
    string GenerateJs(ElementValueTarget targetElement, JsElementObjectName elementToInsertObjectName, JsElementObjectName elementsToAppendObjectName, string parentValueObjectName, string valueObjectName);
}