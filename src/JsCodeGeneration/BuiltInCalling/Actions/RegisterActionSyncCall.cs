namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionSyncCall : IRegisterActionSyncCall
{
    public string Generate(string elementsArg, string eventArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerActionSync({elementsArg},'{eventArg}','{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";
}

