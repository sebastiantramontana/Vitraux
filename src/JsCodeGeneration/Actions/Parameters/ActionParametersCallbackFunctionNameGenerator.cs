using Vitraux.Helpers;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;

internal class ActionParametersCallbackFunctionNameGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : IActionParametersCallbackFunctionNameGenerator
{
    public string Generate()
        => $"pc{atomicAutoNumberGenerator.Next()}";
}
