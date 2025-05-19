using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class InsertionTargetElementsUpdateValueJsGenerator(
    IUpdateValueByPopulatingElementsCall updateByPopulatingElementsCall,
    IToChildQueryFunctionCall toChildQueryFunctionCall,
    IUpdateChildElementsFunctionCall updateChildElementsFunctionCall)
    : IInsertionTargetElementsUpdateValueJsGenerator
{
    public string GenerateJsCode(ElementValueTarget targetElement, IEnumerable<JsObjectName> associatedElements, string parentValueObjectName, string valueObjectName)
        => associatedElements
            .Aggregate(new StringBuilder(), (sb, jsObjName) => sb.AppendLine(CreateUpdateByInsertingElementsFunctionCall(targetElement, jsObjName, parentValueObjectName, valueObjectName)))
            .ToString()
            .TrimEnd();

    private string CreateUpdateByInsertingElementsFunctionCall(ElementValueTarget targetElement, JsObjectName jsObjectName, string parentValueObjectName, string valueObjectName)
    {
        var targetChildElement = targetElement.Insertion?.TargetChildElement as ElementQuerySelectorString;

        if (targetChildElement == null)
            return string.Empty;

        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate(targetChildElement.Query);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(targetElement, parentValueObjectName, valueObjectName);
        var updateByInsertingElementsCallCode = updateByPopulatingElementsCall.Generate(jsObjectName.Name, targetElement.AppendToJsObjNameName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode);

        return $"{updateByInsertingElementsCallCode};";
    }
}