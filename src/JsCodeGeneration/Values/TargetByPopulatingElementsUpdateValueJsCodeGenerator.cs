using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetByPopulatingElementsUpdateValueJsCodeGenerator(
    IUpdateValueByPopulatingElementsCall updateByPopulatingElementsCall,
    IToChildQueryFunctionCall toChildQueryFunctionCall,
    IUpdateChildElementsFunctionCall updateChildElementsFunctionCall)
    : ITargetByPopulatingElementsUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string parentValueObjectName, string valueObjectName)
        => associatedElements
                .Cast<PopulatingElementObjectName>()
                .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(CreateUpdateByPopulatingElementsFunctionCall(targetElement, ae, parentValueObjectName, valueObjectName)))
                .ToString()
                .TrimEnd();

    private string CreateUpdateByPopulatingElementsFunctionCall(TargetElement targetElement, PopulatingElementObjectName populatingElementObjectName, string parentValueObjectName, string valueObjectName)
    {
        var populatingSelector = populatingElementObjectName.AssociatedSelector as PopulatingElementSelectorBase;
        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate((populatingSelector!.TargetChildElement as ElementQuerySelectorString).Query);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(targetElement, parentValueObjectName, valueObjectName);
        var updateByPopulatingElementsCallCode = updateByPopulatingElementsCall.Generate(populatingElementObjectName.Name, populatingElementObjectName.AppendToName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode);

        return $"{updateByPopulatingElementsCallCode};";
    }
}