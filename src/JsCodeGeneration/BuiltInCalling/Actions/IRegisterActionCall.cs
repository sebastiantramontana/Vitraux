namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal interface IRegisterActionCall
{
    string Generate(string elementsArg, string eventArg, string vmKeyArg, string actionKeyArg, string actionArgsCallbackArg);
}