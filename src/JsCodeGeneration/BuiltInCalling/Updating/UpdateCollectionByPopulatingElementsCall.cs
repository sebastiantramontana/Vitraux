namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateCollectionByPopulatingElementsCall : IUpdateCollectionByPopulatingElementsCall
{
    public string Generate(string appendToElementsArg, string elementToInsertArg, string updateCallbackArg, string collectionArg)
        => $"globalThis.vitraux.updating.UpdateCollectionByPopulatingElements({appendToElementsArg}, {elementToInsertArg}, {updateCallbackArg}, {collectionArg})";
}
