using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    ICodeFormatter codeFormatter,
    IJsElementObjectNamesGenerator jsElementObjectNamesGenerator)
    : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    const string CollectionItemObjectName = "item";
    const string ParentElementObjectName = "p";

    public UpdateCollectionFunctionCallbackInfo GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var elementNamePrefix = GenerateElementNamePrefix(parentObjectName, collectionObjectName);
        var allJsElementObjectNames = jsElementObjectNamesGenerator.Generate(elementNamePrefix, elementObjectPairNames.Target.Data);

        var generatedJs = updateViewJsGenerator.GenerateJs(QueryElementStrategy.Always, elementObjectPairNames.Children, allJsElementObjectNames, CollectionItemObjectName, ParentElementObjectName);
        var functionName = collectionUpdateFunctionNameGenerator.Generate();

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

    private static string GenerateElementNamePrefix(string parentObjectName, string collectionObjectName)
        => $"{parentObjectName.Replace('.', '_')}_{collectionObjectName}";
}