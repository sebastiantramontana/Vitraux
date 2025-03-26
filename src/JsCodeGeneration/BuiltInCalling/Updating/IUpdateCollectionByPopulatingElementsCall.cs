namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateCollectionByPopulatingElementsCall
{
    string Generate(string appendToElementsArg, string elementToInsertArg, string updateCallbackArg, string collectionArg);
}