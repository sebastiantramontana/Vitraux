namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

public class ExecuteUpdateViewFunctionCall : IExecuteUpdateViewFunctionCall
{
    public string Generate(string vmKeyArg, string vmArg)
        => $"await globalThis.vitraux.updating.vmFunctions.executeUpdateViewFunction('{vmKeyArg}', {vmArg})";
}
