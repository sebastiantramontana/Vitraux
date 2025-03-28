using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetByPopulatingElementsUpdateValueJsCodeGenerator(
    IUpdateValueByPopulatingElementsCall updateByPopulatingElementsCall,
    IToChildQueryFunctionCall toChildQueryFunctionCall,
    IUpdateChildElementsFunctionCall updateChildElementsFunctionCall)
    : ITargetByPopulatingElementsUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(ElementTarget targetElement, IEnumerable<ElementObjectName> associatedElements, string parentValueObjectName, string valueObjectName)
        => associatedElements
                .Cast<PopulatingElementObjectName>()
                .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(CreateUpdateByPopulatingElementsFunctionCall(targetElement, ae, parentValueObjectName, valueObjectName)))
                .ToString()
                .TrimEnd();

    private string CreateUpdateByPopulatingElementsFunctionCall(ElementTarget targetElement, PopulatingElementObjectName populatingElementObjectName, string parentValueObjectName, string valueObjectName)
    {
        var populatingSelector = populatingElementObjectName.AssociatedSelector as InsertElementSelectorBase;
        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate((populatingSelector!.TargetChildElement as ElementQuerySelectorString).Query);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(targetElement, parentValueObjectName, valueObjectName);
        var updateByPopulatingElementsCallCode = updateByPopulatingElementsCall.Generate(populatingElementObjectName.Name, populatingElementObjectName.AppendToName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode);

        return $"{updateByPopulatingElementsCallCode};";
    }
}