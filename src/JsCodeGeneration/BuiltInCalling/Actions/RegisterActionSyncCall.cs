namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionSyncCall : IRegisterActionSyncCall
{
    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg)
        => $"globalThis.vitraux.actions.registerActionSync({elementsArg},'{string.Join(",", eventsArg)}','{vmKeyArg}', '{actionKeyArg}')";
}

