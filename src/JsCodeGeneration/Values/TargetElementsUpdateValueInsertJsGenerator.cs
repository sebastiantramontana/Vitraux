using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
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
    public string GenerateJs(ElementValueTarget targetElement, JsObjectName elementToInsertObjectName, JsObjectName elementsToAppendObjectName, string parentValueObjectName, string valueObjectName)
        => targetElement.Insertion!.TargetChildElementsSelector switch
        {
            ElementQuerySelectorString targetChildElementsSelector => GenerateJsByQueryString(targetChildElementsSelector.Query, targetElement.Place, elementToInsertObjectName.Name, elementsToAppendObjectName.Name, parentValueObjectName, valueObjectName),
            ElementQuerySelectorDelegate => string.Empty,
            _ => notImplementedCaseGuard.ThrowException<string>(targetElement.Insertion!.TargetChildElementsSelector)
        };

    private string GenerateJsByQueryString(string toChildQuerySelector, ElementPlace childElementsPlace, string elementToInsertObjectName, string elementsToAppendObjectName, string parentValueObjectName, string valueObjectName)
    {
        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate(toChildQuerySelector);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(childElementsPlace, parentValueObjectName, valueObjectName);
        var updateByInsertingElementsCallCode = updateByInsertingElementsCall.Generate(elementToInsertObjectName, elementsToAppendObjectName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode);

        return $"{updateByInsertingElementsCallCode};";
    }
}