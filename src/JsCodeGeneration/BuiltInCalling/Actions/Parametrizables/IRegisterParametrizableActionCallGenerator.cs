namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal interface IRegisterParametrizableActionCallGenerator
{
    string Generate(string registerActionFunctionName, string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg);
}
