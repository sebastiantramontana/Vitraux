using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collections, IUpdateViewJsGenerator updateViewJsGenerator) 
        => collections
            .Aggregate(new StringBuilder(), (sb, collection)
                => collection
                    .AssociatedElementNames
                    .Aggregate(sb, (sb2, associatedElement) =>
                    {
                        var jsCodeCollection = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, updateViewJsGenerator);
                        var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, jsCodeCollection);

                        return sb2
                            .AppendLine(propertyCheckerJsCode)
                            .AppendLine();
                    })
            )
            .ToString()
            .TrimEnd();
}