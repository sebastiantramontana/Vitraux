namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

internal interface IRegisterParametrizableActionCall
{
    string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg);
}
