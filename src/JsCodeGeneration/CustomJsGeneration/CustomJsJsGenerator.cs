using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.CustomJsGeneration;

internal class CustomJsJsGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator, ICodeFormatter codeFormatter) : ICustomJsJsGenerator
{
    public StringBuilder Generate(StringBuilder jsBuilder, CustomJsTargetBase customJsTarget, string objArg, int indentCount)
    {
        var customJs = customJsTarget.ModuleFrom is not null
            ? GenerateJsImportModuleInfo(customJsTarget)
            : GenerateNoImportModuleInfo(customJsTarget);

        return jsBuilder
            .TryAddLine(AddImportLine, customJs.ImportLine, indentCount)
            .Add(GenerateJsFunctionCall, customJs.FullFunctionPath, objArg, indentCount);
    }

    private StringBuilder AddImportLine(StringBuilder jsBuilder, string importLine, int indentCount)
        => !string.IsNullOrWhiteSpace(importLine)
            ? jsBuilder.Append(codeFormatter.IndentLine(importLine, indentCount))
            : jsBuilder;

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

    private StringBuilder GenerateJsFunctionCall(StringBuilder jsBuilder, string fullFunctionPath, string objArg, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine($"await {fullFunctionPath}({objArg});", indentCount));

}