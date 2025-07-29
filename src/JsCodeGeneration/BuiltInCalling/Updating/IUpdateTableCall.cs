namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateTableCall
{
    string Generate(string tablesArg, int tbodyIndex, string rowToInsertArg, string updateCallbackArg, string collectionArg);
}