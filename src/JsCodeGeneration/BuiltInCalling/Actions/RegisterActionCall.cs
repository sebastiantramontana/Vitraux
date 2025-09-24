namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
internal class RegisterActionCall : IRegisterActionCall
{
    public string Generate(string elementsArg, string eventArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"globalThis.vitraux.actions.registerAction({elementsArg},{eventArg}),{vmKeyArg}, {actionKeyArg}, {actionArgsCallbackArg})";
}

