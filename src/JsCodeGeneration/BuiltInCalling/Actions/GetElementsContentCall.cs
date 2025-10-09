namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class GetElementsContentCall : IGetElementsContentCall
{
    public string Generate(string elementsParametersArg)
        => $"globalThis.vitraux.actions.dom.getElementsContent({elementsParametersArg})";
}
