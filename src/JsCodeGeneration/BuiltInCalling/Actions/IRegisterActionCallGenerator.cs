namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal interface IRegisterActionCallGenerator
{
    string Generate(string registerActionFunctionName, string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg);
}
