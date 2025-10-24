namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionAsyncCall(IRegisterActionCallGenerator generator) : IRegisterActionAsyncCall
{
    private const string FunctionName = "registerActionAsync";

    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg)
        => generator.Generate(FunctionName, elementsArg, eventsArg, vmKeyArg, actionKeyArg);
}