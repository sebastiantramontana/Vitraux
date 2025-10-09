namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionSyncCall : IRegisterActionSyncCall
{
    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerActionSync({elementsArg},'{string.Join(",", eventsArg)}','{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";
}

