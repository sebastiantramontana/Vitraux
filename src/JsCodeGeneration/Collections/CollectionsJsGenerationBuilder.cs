using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectNameWithElements> collections, IUpdateViewJsGenerator updateViewJsGenerator)
        => collections
            .Aggregate(new StringBuilder(), (sb, collection)
                => collection
                    .AssociatedElementNames
                    .Aggregate(sb, (sb2, associatedElement) =>
                    {
                        var updateCollectionjsCode = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, updateViewJsGenerator);
                        var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, updateCollectionjsCode);

                        return sb2
                            .AppendLine(propertyCheckerJsCode)
                            .AppendLine();
                    }))
            .ToString()
            .TrimEnd();
}



