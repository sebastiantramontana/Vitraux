using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal class PropertyCheckerJsCodeGeneration(ICodeFormatter codeFormatter) : IPropertyCheckerJsCodeGeneration
{
    public string GenerateJs(string objectParentName, string valueObjectName, string jsCodeBlock)
        => new StringBuilder()
            .AppendLine($"if({objectParentName}.{valueObjectName}) {{")
            .AppendLine(codeFormatter.Indent(jsCodeBlock).TrimEnd())
            .AppendLine("}")
            .ToString();
}