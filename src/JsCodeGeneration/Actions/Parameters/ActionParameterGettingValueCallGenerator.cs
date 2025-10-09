using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class ActionParameterGettingValueCallGenerator(
    IGetInputValueCall getInputValueCall,
    IGetElementsContentCall getElementsContentCall,
    IGetElementsAttributeCall getElementsAttributeCall,
    INotImplementedCaseGuard notImplementedCaseGuard) : IActionParameterGettingValueCallGenerator
{
    public string GenerateJs(string jsParameterObjectName, ElementPlace elementPlace)
        => elementPlace switch
        {
            ValuePropertyElementPlace => getInputValueCall.Generate(jsParameterObjectName),
            ContentElementPlace => getElementsContentCall.Generate(jsParameterObjectName),
            AttributeElementPlace attribute => getElementsAttributeCall.Generate(jsParameterObjectName, attribute.Attribute),
            _ => notImplementedCaseGuard.ThrowException<string>(elementPlace)
        };
}
