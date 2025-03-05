namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateTableCall
{
    string Generate(string tablesArg, string rowToInsertArg, string updateCallbackArg, string collectionArg);
}