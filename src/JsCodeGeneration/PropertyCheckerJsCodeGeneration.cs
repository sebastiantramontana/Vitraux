using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration;

internal class PropertyCheckerJsCodeGeneration(IIsValueValidCall isValueValidCall, ICodeFormatter codeFormatter) : IPropertyCheckerJsCodeGeneration
{
    public StringBuilder GenerateBeginCheckJs(StringBuilder jsBuilder, string objectParentName, string valueObjectName, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine(GenerateOpenIfSentenceJs(objectParentName, valueObjectName), indentCount));

    public StringBuilder GenerateEndCheckJs(StringBuilder jsBuilder, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine("}", indentCount));

    private string GenerateOpenIfSentenceJs(string objectParentName, string valueObjectName)
        => $"if({isValueValidCall.Generate($"{objectParentName}.{valueObjectName}")}) {{";
}