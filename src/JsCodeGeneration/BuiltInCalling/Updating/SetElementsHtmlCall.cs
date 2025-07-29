namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class SetElementsHtmlCall : ISetElementsHtmlCall
{
    public string Generate(string elementsArg, string contentArg)
        => $"globalThis.vitraux.updating.dom.setElementsHtml({elementsArg}, {contentArg})";
}
