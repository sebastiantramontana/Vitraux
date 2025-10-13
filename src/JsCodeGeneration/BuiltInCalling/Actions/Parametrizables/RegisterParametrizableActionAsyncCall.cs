namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;
internal class RegisterParametrizableActionAsyncCall : IRegisterParametrizableActionAsyncCall
{
    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerParametrizableActionAsync({elementsArg},'[{string.Join(",", eventsArg)}]','{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";
}
