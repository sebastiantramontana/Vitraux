namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateCollectionByPopulatingElementsCall : IUpdateCollectionByPopulatingElementsCall
{
    public string Generate(string appendToElementsArg, string elementToInsertArg, string updateCallbackArg, string collectionArg)
        => $"await globalThis.vitraux.updating.dom.updateCollectionByPopulatingElements({appendToElementsArg}, {elementToInsertArg}, {updateCallbackArg}, {collectionArg})";
}
