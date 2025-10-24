namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionCallGenerator : IRegisterActionCallGenerator
{
    private const string FunctionJsNamespacePath = "globalThis.vitraux.actions";

    public string Generate(string registerActionFunctionName, string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg)
        => $"{FunctionJsNamespacePath}.{registerActionFunctionName}({elementsArg}, [{string.Join(",", QuoteArgs(eventsArg))}], '{vmKeyArg}', '{actionKeyArg}')";

    private static IEnumerable<string> QuoteArgs(IEnumerable<string> args)
        => args.Select(a => $"'{a}'");
}
