using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsUpdateValueInsertJsGenerator(
    IUpdateValueByInsertingElementsCall updateByInsertingElementsCall,
    IToChildQueryFunctionCall toChildQueryFunctionCall,
    IUpdateChildElementsFunctionCall updateChildElementsFunctionCall,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : ITargetElementsUpdateValueInsertJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, ElementValueTarget targetElement, JsElementObjectName elementToInsertObjectName, JsElementObjectName elementsToAppendObjectName, string parentValueObjectName, string valueObjectName, int indentCount)
        => targetElement.Insertion!.TargetChildElementsSelector switch
        {
            ElementQuerySelectorString targetChildElementsSelector => GenerateJsByQueryString(jsBuilder, targetChildElementsSelector.Query, targetElement.Place, elementToInsertObjectName.Name, elementsToAppendObjectName.Name, parentValueObjectName, valueObjectName, indentCount),
            _ => notImplementedCaseGuard.ThrowException<StringBuilder>(targetElement.Insertion!.TargetChildElementsSelector)
        };

    private StringBuilder GenerateJsByQueryString(StringBuilder jsBuilder, string toChildQuerySelector, ElementPlace childElementsPlace, string elementToInsertObjectName, string elementsToAppendObjectName, string parentValueObjectName, string valueObjectName, int indentCount)
    {
        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate(toChildQuerySelector);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(childElementsPlace, parentValueObjectName, valueObjectName);

        return jsBuilder
                .Add(updateByInsertingElementsCall.Generate, elementToInsertObjectName, elementsToAppendObjectName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode, indentCount)
                .Append(';');
    }
}