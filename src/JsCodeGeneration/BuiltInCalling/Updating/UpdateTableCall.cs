namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateTableCall : IUpdateTableCall
{
    public string Generate(string tablesArg, string rowToInsertArg, string updateCallbackArg, string collectionArg)
        => $"await globalThis.vitraux.updating.dom.updateTable({tablesArg}, {rowToInsertArg}, {updateCallbackArg}, {collectionArg})";
}
