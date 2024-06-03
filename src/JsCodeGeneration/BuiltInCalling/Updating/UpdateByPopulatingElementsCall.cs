using System.Text;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal class UpdateByPopulatingElementsCall(ICodeFormatting codeFormatting) : IUpdateByPopulatingElementsCall
{
    public string Generate(string elementObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateElementChildFunctionCall)
        => new StringBuilder()
            .AppendLine($"globalThis.vitraux.updating.UpdateByPopulatingElements(")
            .AppendLine(codeFormatting.Indent($"{elementObjectName},"))
            .AppendLine(codeFormatting.Indent($"{appendToElementsObjectName},"))
            .AppendLine(codeFormatting.Indent($"{toChildQueryFunctionCall},"))
            .AppendLine(codeFormatting.Indent($"{updateElementChildFunctionCall});"))
            .ToString();
}
