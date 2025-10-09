using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal interface IActionParameterGettingValueCallGenerator
{
    string GenerateJs(string jsParameterObjectName, ElementPlace elementPlace);
}
