using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateChildElementsFunctionCall(
    ISetElementsAttributeCall setElementsAttributeCall,
    ISetElementsContentCall setElementsContentCall)
    : IUpdateChildElementsFunctionCall
{
    public string Generate(ElementValueTarget toChildTargetElement, string parentValueObjectName, string valueObjectName)
    {
        const string targetTemplateChildElements = "targetChildElements";
        var fullValueObject = $"{parentValueObjectName}.{valueObjectName}";

        return $"({targetTemplateChildElements}) => " +
            toChildTargetElement.Place switch
            {
                AttributeElementPlace => setElementsAttributeCall.Generate(targetTemplateChildElements, toChildTargetElement.Place.Value, fullValueObject),
                ContentElementPlace => setElementsContentCall.Generate(targetTemplateChildElements, fullValueObject),
                _ => throw new NotImplementedException($"{toChildTargetElement.Place.GetType().Name} not implemented in {nameof(UpdateChildElementsFunctionCall)}"),
            };
    }
}

