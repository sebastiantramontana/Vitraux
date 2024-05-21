using System.Text;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating
{
    internal class UpdateByTemplateCall(ICodeFormatting codeFormatting) : IUpdateByTemplateCall
    {
        public string Generate(string templateElementAsArrayObjectName, string appendToElementsObjectName, string toChildQueryFunctionCall, string updateTemplateChildFunctionCall)
            => new StringBuilder()
                .AppendLine($"globalThis.vitraux.updating.UpdateByTemplate(")
                .AppendLine(codeFormatting.Indent($"{templateElementAsArrayObjectName}[0],"))
                .AppendLine(codeFormatting.Indent($"{appendToElementsObjectName},"))
                .AppendLine(codeFormatting.Indent($"{toChildQueryFunctionCall},"))
                .AppendLine(codeFormatting.Indent($"{updateTemplateChildFunctionCall});"))
                .ToString();
    }
}
