using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetByPopulatingElementsUpdateValueJsCodeGenerator(
    IUpdateByPopulatingElementsCall updateByPopulatingElementsCall,
    IToChildQueryFunctionCall toChildQueryFunctionCall,
    IUpdateChildElementsFunctionCall updateChildElementsFunctionCall,
    ICodeFormatting codeFormatting)
    : ITargetByPopulatingElementsUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string valueObjectName) 
        => associatedElements
                .Cast<PopulatingElementObjectName>()
                .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(CreateUpdateByPopulatingElementsFunctionCall(targetElement, ae, valueObjectName)))
                .ToString();

    private string CreateUpdateByPopulatingElementsFunctionCall(TargetElement targetElement, PopulatingElementObjectName populatingElementObjectName, string valueObjectName)
    {
        var populatingSelector = populatingElementObjectName.AssociatedSelector as PopulatingElementSelectorBase;
        var toChildQueryFunctionCallCode = toChildQueryFunctionCall.Generate((populatingSelector!.TargetChildElement as ElementQuerySelectorString).Query);
        var updateChildElementsFunctionCallCode = updateChildElementsFunctionCall.Generate(targetElement, valueObjectName);
        var updateByPopulatingElementsCallCode = updateByPopulatingElementsCall.Generate(populatingElementObjectName.Name, populatingElementObjectName.AppendToName, toChildQueryFunctionCallCode, updateChildElementsFunctionCallCode);

        return codeFormatting.Indent(updateByPopulatingElementsCallCode);
    }
}