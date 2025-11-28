namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal class RegisterParametrizableActionCallGenerator : IRegisterParametrizableActionCallGenerator
{
    private const string FunctionJsNamespacePath = "globalThis.vitraux.actions.registration";

    public string Generate(string registerActionFunctionName, string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => $"{FunctionJsNamespacePath}.{registerActionFunctionName}({elementsArg}, [{string.Join(",", QuoteArgs(eventsArg))}], '{vmKeyArg}', '{actionKeyArg}', {actionArgsCallbackArg})";

    private static IEnumerable<string> QuoteArgs(IEnumerable<string> args)
        => args.Select(a => $"'{a}'");
}
