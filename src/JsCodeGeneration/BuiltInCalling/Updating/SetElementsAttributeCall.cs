namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating
{
    internal class SetElementsAttributeCall : ISetElementsAttributeCall
    {
        public string Generate(string elementsArg, string attribute, string valueArg)
            => $"globalThis.vitraux.updating.setElementsAttribute({elementsArg}, '{attribute}', {valueArg})";
    }
}
