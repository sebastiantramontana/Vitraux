namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
internal class RegisterActionAsyncCall : IRegisterActionAsyncCall
{
    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerActionAsync({elementsArg},'[{string.Join(",", eventsArg)}]','{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";
}