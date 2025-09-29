namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
internal class RegisterActionAsyncCall : IRegisterActionAsyncCall
{
    public string Generate(string elementsArg, string eventArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerActionAsync({elementsArg},{eventArg}),{vmKeyArg}, {actionKeyArg}, {actionArgsCallbackArg})";
}