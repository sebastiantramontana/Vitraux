using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collections, IUpdateViewJsGenerator updateViewJsGenerator)
        => collections.Aggregate(new StringBuilder(), (sb, collection)
            =>
            {
                var updateCollectionJs = collection
                        .AssociatedNames
                        .Aggregate(new StringBuilder(), (sb2, associatedElement) =>
                        {
                            var jsCodeCollection = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, updateViewJsGenerator);

                            return sb2
                                .AppendLine(jsCodeCollection)
                                .AppendLine();
                        })
                        .ToString()
                        .TrimEnd();

                var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, updateCollectionJs);

                return sb
                    .AppendLine(propertyCheckerJsCode)
                    .AppendLine();
            })
            .ToString()
            .TrimEnd();
}