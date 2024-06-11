using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateChildElementsFunctionCall(
    ISetElementsAttributeCall setElementsAttributeCall,
    ISetElementsContentCall setElementsContentCall) 
    : IUpdateChildElementsFunctionCall
{
    public string Generate(TargetElement toChildTargetElement, string valueObjectName)
    {
        const string targetTemplateChildElements = "targetChildElements";
        var fullValueObject = $"vm.{valueObjectName}";

        return $"({targetTemplateChildElements}) => " +
            toChildTargetElement.Place.ElementPlacing switch
            {
                ElementPlacing.Attribute => setElementsAttributeCall.Generate(targetTemplateChildElements, toChildTargetElement.Place.Value, fullValueObject),
                ElementPlacing.Content => setElementsContentCall.Generate(targetTemplateChildElements, fullValueObject),
                _ => throw new NotImplementedException($"{toChildTargetElement.Place.ElementPlacing} not implemented in {nameof(UpdateChildElementsFunctionCall)}"),
            };
    }
}

