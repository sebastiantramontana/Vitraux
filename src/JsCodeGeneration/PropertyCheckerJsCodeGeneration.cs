using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration;

internal class PropertyCheckerJsCodeGeneration(IIsValueValidCall isValueValidCall, ICodeFormatter codeFormatter) : IPropertyCheckerJsCodeGeneration
{
    public string GenerateJs(string objectParentName, string valueObjectName, string jsCodeBlock)
        => new StringBuilder()
            .AppendLine($"if({isValueValidCall.Generate($"{objectParentName}.{valueObjectName}")}) {{")
            .AppendLine(codeFormatter.Indent(jsCodeBlock))
            .Append('}')
            .ToString();
}