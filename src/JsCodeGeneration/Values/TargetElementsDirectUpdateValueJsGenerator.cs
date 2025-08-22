using Vitraux.Helpers;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsDirectUpdateValueJsGenerator(
    IElementPlaceAttributeJsGenerator attributeGenerator,
    IElementPlaceContentJsGenerator contentGenerator,
    IElementPlaceHtmlJsGenerator htmlGenerator,
    INotImplementedCaseGuard notImplementedPlaceGuard)
    : ITargetElementsDirectUpdateValueJsGenerator
{
    public string GenerateJs(string jsObjectName, ElementPlace elementPlace, string parentValueObjectName, string valueObjectName)
        => elementPlace switch
        {
            AttributeElementPlace attrPlace => attributeGenerator.Generate(attrPlace.Attribute, jsObjectName, parentValueObjectName, valueObjectName),
            ContentElementPlace => contentGenerator.Generate(jsObjectName, parentValueObjectName, valueObjectName),
            HtmlElementPlace => htmlGenerator.Generate(jsObjectName, parentValueObjectName, valueObjectName),
            _ => notImplementedPlaceGuard.ThrowException<string>(elementPlace),
        };
}
