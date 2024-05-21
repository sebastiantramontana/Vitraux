using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueCheckJsCodeGeneration : IValueCheckJsCodeGeneration
{
    public string GenerateJsCode(string valueObjectName, string jsCodeBlock)
        => new StringBuilder()
            .AppendLine($"if(vm.{valueObjectName}) {{")
            .Append(jsCodeBlock)
            .AppendLine("}")
            .ToString();
}