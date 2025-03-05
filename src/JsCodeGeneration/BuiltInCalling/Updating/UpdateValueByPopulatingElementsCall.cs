using System.Text;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateValueByPopulatingElementsCall(ICodeFormatter codeFormatting) : IUpdateValueByPopulatingElementsCall
{
    public string Generate(string elementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateElementChildFunctionCall)
        => new StringBuilder()
            .AppendLine($"globalThis.vitraux.updating.UpdateValueByPopulatingElements(")
            .AppendLine(codeFormatting.Indent($"{elementObjectName},"))
            .AppendLine(codeFormatting.Indent($"{appendToElementsObjectName},"))
            .AppendLine(codeFormatting.Indent($"{toChildQueryFunctionCall},"))
            .Append(codeFormatting.Indent($"{updateElementChildFunctionCall})"))
            .ToString();
}
