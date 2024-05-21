using System.Runtime.InteropServices.JavaScript;

namespace Vitraux.JsCodeGeneration;

internal partial class JsCodeExecutor : IJsCodeExecutor
{
    [JSImport("globalThis.vitraux.ExecuteCode")]
    internal static partial void ExcuteCodeImport(string code);

    public void Excute(string code)
    {
        ExcuteCodeImport(code);
    }
}

