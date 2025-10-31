using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(ICodeFormatter codeFormatter) : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    const string CollectionItemObjectName = "item";
    const string ParentElementObjectName = "p";

    public StringBuilder GenerateJs(StringBuilder jsBuilder, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator, string functionName, int currentIndentCount) 
        => jsBuilder
            .AppendLine(codeFormatter.IndentLine(GenerateFunctionHeader(functionName), currentIndentCount))
            .AppendLine(codeFormatter.IndentLine("{", currentIndentCount))
            .TryAddLine(updateViewJsGenerator.GenerateJs, QueryElementStrategy.Always, elementObjectPairNames.Children, CollectionItemObjectName, ParentElementObjectName, currentIndentCount + 1)
            .Append(codeFormatter.IndentLine("}", currentIndentCount));

    private static string GenerateFunctionHeader(string functionName)
        => $"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>";
}