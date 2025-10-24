namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal class RegisterParametrizableActionSyncCall(IRegisterParametrizableActionCallGenerator callGenerator) : IRegisterParametrizableActionSyncCall
{
    private const string FunctionName = "registerParametrizableActionSync";

    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => callGenerator.Generate(FunctionName, elementsArg, eventsArg, vmKeyArg, actionKeyArg, actionArgsCallbackArg);
}