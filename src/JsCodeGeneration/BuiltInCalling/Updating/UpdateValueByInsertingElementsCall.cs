using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateValueByInsertingElementsCall(ICodeFormatter codeFormatting) : IUpdateValueByInsertingElementsCall
{
    public StringBuilder Generate(StringBuilder jsBuilder, string elementToInsertObjectName, string appendToElementsObjectName, string queryChildrenFunctionCall, string updateChildElementsFunctionCall, int indentCount)
        => jsBuilder
            .AppendLine(codeFormatting.IndentLine($"globalThis.vitraux.updating.dom.updateValueByInsertingElements(", indentCount))
            .AppendLine(codeFormatting.IndentLine($"{elementToInsertObjectName},", indentCount + 1))
            .AppendLine(codeFormatting.IndentLine($"{appendToElementsObjectName},", indentCount + 1))
            .AppendLine(codeFormatting.IndentLine($"{queryChildrenFunctionCall},", indentCount + 1))
            .Append(codeFormatting.IndentLine($"{updateChildElementsFunctionCall})", indentCount + 1));
}