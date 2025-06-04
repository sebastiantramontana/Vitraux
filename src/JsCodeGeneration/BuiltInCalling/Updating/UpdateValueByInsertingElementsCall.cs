using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateValueByInsertingElementsCall(ICodeFormatter codeFormatting) : IUpdateValueByInsertingElementsCall
{
    public string Generate(string elementToInsertObjectName, string appendToElementsObjectName, string queryChildrenFunctionCall, string updateChildElementsFunctionCall)
        => new StringBuilder()
            .AppendLine($"globalThis.vitraux.updating.dom.updateValueByInsertingElements(")
            .AppendLine(codeFormatting.Indent($"{elementToInsertObjectName},"))
            .AppendLine(codeFormatting.Indent($"{appendToElementsObjectName},"))
            .AppendLine(codeFormatting.Indent($"{queryChildrenFunctionCall},"))
            .Append(codeFormatting.Indent($"{updateChildElementsFunctionCall})"))
            .ToString();
}