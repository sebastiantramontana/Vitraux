namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal interface IRegisterActionCall
{
    string Generate(string elementsArg, IEnumerable<string> eventsArg, string vmKeyArg, string actionKeyArg);
}