namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class SetElementsContentCall : ISetElementsContentCall
{
    public string Generate(string elementsArg, string contentArg)
        => $"globalThis.vitraux.updating.setElementsContent({elementsArg}, {contentArg})";
}
