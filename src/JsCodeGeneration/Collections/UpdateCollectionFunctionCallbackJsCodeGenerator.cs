using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    ICodeFormatter codeFormatter) : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    public UpdateCollectionFunctionCallbackInfo GenerateJsCode(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator)
    {
        const string CollectionItemObjectName = "item";
        const string ParentElementObjectName = "p";

        var functionName = collectionUpdateFunctionNameGenerator.Generate();
        var elementNamePrefix = $"{parentObjectName.Replace('.', '_')}_{collectionObjectName}";
        var generatedJs = jsGenerator.GenerateJs(elementObjectPairNames.Target.Data, QueryElementStrategy.Always, CollectionItemObjectName, ParentElementObjectName, elementNamePrefix);

        var jsCode = new StringBuilder()
            .AppendLine($"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>")
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(generatedJs))
            .Append('}')
            .ToString();

        return new UpdateCollectionFunctionCallbackInfo(functionName, jsCode);
    }
}



