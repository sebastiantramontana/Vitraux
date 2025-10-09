namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class GetElementsAttributeCall : IGetElementsAttributeCall
{
    public string Generate(string elementsParametersArg, string attributeArg)
        => $"globalThis.vitraux.actions.dom.getElementsAttribute({elementsParametersArg},'{attributeArg}')";
}