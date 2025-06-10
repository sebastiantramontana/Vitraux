using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    ICodeFormatter codeFormatter,
    IJsObjectNamesGenerator jsObjectNamesGenerator)
    : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    const string CollectionItemObjectName = "item";
    const string ParentElementObjectName = "p";

    public UpdateCollectionFunctionCallbackInfo GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var functionName = collectionUpdateFunctionNameGenerator.Generate();
        var elementNamePrefix = $"{parentObjectName.Replace('.', '_')}_{collectionObjectName}";
        var objectNamesGroup = jsObjectNamesGenerator.Generate(elementObjectPairNames.Target.Data, elementNamePrefix);

        var generatedJs = updateViewJsGenerator.GenerateJs(QueryElementStrategy.Always, objectNamesGroup, CollectionItemObjectName, ParentElementObjectName);

        var jsCode = new StringBuilder()
            .AppendLine(GenerateFunctionHeader(functionName))
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(generatedJs.JsCode))
            .Append('}')
            .ToString();

        return new(functionName, jsCode, generatedJs.ViewModelSerializationData);
    }

    private static string GenerateFunctionHeader(string functionName) 
        => $"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>";
}