namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal class RegisterActionSyncCall(IRegisterActionCallGenerator generator) : IRegisterActionSyncCall
{
    private const string FunctionName = "registerActionSync";

    public string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg)
        => generator.Generate(FunctionName, elementsArg, eventsArg, vmKeyArg, actionKeyArg);
}

