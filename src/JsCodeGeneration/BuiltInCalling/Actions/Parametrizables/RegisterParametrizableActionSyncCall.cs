namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal class RegisterParametrizableActionSyncCall : IRegisterParametrizableActionSyncCall
{
    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerParametrizableActionSync({elementsArg},'{string.Join(",", eventsArg)}','{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";
}