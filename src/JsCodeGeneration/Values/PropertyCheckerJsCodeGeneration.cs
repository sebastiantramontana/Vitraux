using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal class PropertyCheckerJsCodeGeneration : IPropertyCheckerJsCodeGeneration
{
    public string GenerateJsCode(string objectParentName, string valueObjectName, string jsCodeBlock)
        => new StringBuilder()
            .AppendLine($"if({objectParentName}.{valueObjectName}) {{")
            .Append(jsCodeBlock)
            .AppendLine("}")
            .ToString();
}