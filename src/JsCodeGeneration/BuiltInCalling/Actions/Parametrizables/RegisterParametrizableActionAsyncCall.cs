namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal class RegisterParametrizableActionAsyncCall(IRegisterParametrizableActionCallGenerator callGenerator) : IRegisterParametrizableActionAsyncCall
{
    private const string FunctionName = "registerParametrizableActionAsync";

    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg)
        => callGenerator.Generate(FunctionName, elementsArg, eventsArg, vmKeyArg, actionKeyArg, actionArgsCallbackArg);
}
