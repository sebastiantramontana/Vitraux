namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class GetInputValueCall : IGetInputValueCall
{
    public string Generate(string elementsParametersArg)
        => $"globalThis.vitraux.actions.dom.getInputsValue({elementsParametersArg})";
}
