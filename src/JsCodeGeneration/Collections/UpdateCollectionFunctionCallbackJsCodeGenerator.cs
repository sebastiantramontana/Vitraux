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
    public UpdateCollectionFunctionCallbackInfo GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        const string CollectionItemObjectName = "item";
        const string ParentElementObjectName = "p";

        var functionName = collectionUpdateFunctionNameGenerator.Generate();
        var elementNamePrefix = $"{parentObjectName.Replace('.', '_')}_{collectionObjectName}";
        var objectNamesGroup = jsObjectNamesGenerator.Generate(elementObjectPairNames.Target.Data, elementNamePrefix);

        var generatedJs = updateViewJsGenerator.GenerateJs(QueryElementStrategy.Always, objectNamesGroup, CollectionItemObjectName, ParentElementObjectName);

        var jsCode = new StringBuilder()
            .AppendLine($"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>")
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(generatedJs))
            .Append('}')
            .ToString();

        return new UpdateCollectionFunctionCallbackInfo(functionName, jsCode);
    }
}



