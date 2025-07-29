namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateTableCall : IUpdateTableCall
{
    public string Generate(string tablesArg, int tbodyIndex, string rowToInsertArg, string updateCallbackArg, string collectionArg)
        => $"await globalThis.vitraux.updating.dom.updateTable({tablesArg}, {tbodyIndex}, {rowToInsertArg}, {updateCallbackArg}, {collectionArg})";
}
