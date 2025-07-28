using System.Text;
using Vitraux.Helpers;
using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.CustomJsGeneration;

internal class CustomJsJsGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : ICustomJsJsGenerator
{
    public string Generate(CustomJsTargetBase customJsTarget, string objArg)
    {
        var customJs = customJsTarget.ModuleFrom is not null
            ? GenerateJsImportModuleInfo(customJsTarget)
            : GenerateNoImportModuleInfo(customJsTarget);

        return new StringBuilder()
            .AppendLine(customJs.ImportLine)
            .Append(GenerateJsFunctionCall(customJs.FullFunctionPath, objArg))
            .ToString()
            .TrimStart();
    }

    private static (string ImportLine, string FullFunctionPath) GenerateNoImportModuleInfo(CustomJsTargetBase customJsTarget)
        => (string.Empty, customJsTarget.FunctionName);

    private (string ImportLine, string FullFunctionPath) GenerateJsImportModuleInfo(CustomJsTargetBase customJsTarget)
    {
        var moduleName = $"m{atomicAutoNumberGenerator.Next()}";

        var importLine = GenerateJsImportModuleCall(moduleName, customJsTarget.ModuleFrom!);
        var fullFunctionPath = $"{moduleName}." + customJsTarget.FunctionName;

        return (importLine, fullFunctionPath);
    }

    private static string GenerateJsImportModuleCall(string moduleName, Uri moduleUri)
        => $"const {moduleName} = await import(\'{moduleUri}\');";

    private static string GenerateJsFunctionCall(string fullFunctionPath, string objArg)
        => $"await {fullFunctionPath}({objArg});";

}