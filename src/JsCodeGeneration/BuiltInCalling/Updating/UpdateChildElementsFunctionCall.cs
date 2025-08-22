using Vitraux.Helpers;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateChildElementsFunctionCall(
    ISetElementsAttributeCall setElementsAttributeCall,
    ISetElementsContentCall setElementsContentCall,
    ISetElementsHtmlCall setElementsHtmlCall,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IUpdateChildElementsFunctionCall
{
    public string Generate(ElementPlace chilElementsPlace, string parentValueObjectName, string valueObjectName)
    {
        const string targetTemplateChildElements = "targetChildElements";
        var fullValueObjectName = $"{parentValueObjectName}.{valueObjectName}";

        return $"({targetTemplateChildElements}) => " +
            chilElementsPlace switch
            {
                AttributeElementPlace attributeElementPlace => setElementsAttributeCall.Generate(targetTemplateChildElements, attributeElementPlace.Attribute, fullValueObjectName),
                ContentElementPlace => setElementsContentCall.Generate(targetTemplateChildElements, fullValueObjectName),
                HtmlElementPlace => setElementsHtmlCall.Generate(targetTemplateChildElements, fullValueObjectName),
                _ => notImplementedCaseGuard.ThrowException<string>(chilElementsPlace)
            };
    }
}

