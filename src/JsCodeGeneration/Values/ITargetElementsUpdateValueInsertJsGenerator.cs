using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsUpdateValueInsertJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, ElementValueTarget targetElement, JsElementObjectName elementToInsertObjectName, JsElementObjectName elementsToAppendObjectName, string parentValueObjectName, string valueObjectName, int indentCount);
}