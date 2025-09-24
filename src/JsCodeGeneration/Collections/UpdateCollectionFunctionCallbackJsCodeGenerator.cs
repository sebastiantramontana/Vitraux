using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    ICodeFormatter codeFormatter)
    : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    const string CollectionItemObjectName = "item";
    const string ParentElementObjectName = "p";

    public FunctionCallbackInfo GenerateJs(JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var functionName = collectionUpdateFunctionNameGenerator.Generate();
        var generatedJs = updateViewJsGenerator.GenerateJs(QueryElementStrategy.Always, elementObjectPairNames.Children, CollectionItemObjectName, ParentElementObjectName);

        var jsCode = new StringBuilder()
            .AppendLine(GenerateFunctionHeader(functionName))
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(generatedJs))
            .Append('}')
            .ToString();

        return new(functionName, jsCode);
    }

    private static string GenerateFunctionHeader(string functionName)
        => $"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>";
}