using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementTemplateUpdateValueJsCodeGenerator(
    IUpdateByTemplateCall updateByTemplateCall,
    IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
    ISetElementsAttributeCall setElementsAttributeCall,
    ISetElementsContentCall setElementsContentCall,
    ICodeFormatting codeFormatting)
    : ITargetElementTemplateUpdateValueJsCodeGenerator
{
    public string GenerateJsCode(TargetElement targetElement, IEnumerable<ElementObjectName> associatedElements, string valueObjectName)
        => associatedElements
            .Cast<ElementTemplateObjectName>()
            .Aggregate(new StringBuilder(), (sb, ae) => sb.AppendLine(CreateUpdateByTemplateFunctionCall(targetElement, ae, valueObjectName)))
            .ToString();

    private string CreateUpdateByTemplateFunctionCall(TargetElement targetElement, ElementTemplateObjectName elementTemplateObjectName, string valueObjectName)
    {
        var templateSelector = elementTemplateObjectName.AssociatedSelector as ElementTemplateSelector;

        var toChildQueryFunctionCall = GenerateToChildQueryFunctionCall(templateSelector!.TargetChildElement.Value);
        var updateTemplateChildFunctionCall = GenerateUpdateTemplateChildFunctionCall(targetElement, valueObjectName);

        return codeFormatting.Indent(updateByTemplateCall.Generate(elementTemplateObjectName.Name, elementTemplateObjectName.AppendToName, toChildQueryFunctionCall, updateTemplateChildFunctionCall));
    }

    private string GenerateToChildQueryFunctionCall(string toChildQuerySelector)
    {
        const string templateContentAsParentName = "templateContent";
        return $"({templateContentAsParentName}) => {getElementsByQuerySelectorCall.Generate(templateContentAsParentName, toChildQuerySelector)}";
    }

    private string GenerateUpdateTemplateChildFunctionCall(TargetElement toChildTargetElement, string valueObjectName)
    {
        const string targetTemplateChildElements = "targetTemplateChildElements";
        var fullValueObject = $"vm.{valueObjectName}";

        return $"({targetTemplateChildElements}) => " +
            toChildTargetElement.Place.ElementPlacing switch
            {
                ElementPlacing.Attribute => setElementsAttributeCall.Generate(targetTemplateChildElements, toChildTargetElement.Place.Value, fullValueObject),
                ElementPlacing.Content => setElementsContentCall.Generate(targetTemplateChildElements, fullValueObject),
                _ => throw new NotImplementedException($"{toChildTargetElement.Place.ElementPlacing} not implemented in {nameof(GenerateUpdateTemplateChildFunctionCall)}"),
            };
    }
}