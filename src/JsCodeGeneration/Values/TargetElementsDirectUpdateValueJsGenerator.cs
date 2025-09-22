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
    public string GenerateJs(string jsElementObjectName, ElementPlace elementPlace, string parentValueObjectName, string valueObjectName)
        => elementPlace switch
        {
            AttributeElementPlace attrPlace => attributeGenerator.Generate(attrPlace.Attribute, jsElementObjectName, parentValueObjectName, valueObjectName),
            ContentElementPlace => contentGenerator.Generate(jsElementObjectName, parentValueObjectName, valueObjectName),
            HtmlElementPlace => htmlGenerator.Generate(jsElementObjectName, parentValueObjectName, valueObjectName),
            _ => notImplementedPlaceGuard.ThrowException<string>(elementPlace),
        };
}
