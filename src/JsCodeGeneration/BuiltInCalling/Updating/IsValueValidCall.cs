namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class IsValueValidCall : IIsValueValidCall
{
    public string Generate(string valueArg)
        => $"globalThis.vitraux.updating.utils.isValueValid({valueArg})";
}
