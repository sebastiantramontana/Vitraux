using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public StringBuilder BuildJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullCollectionObjectName> collections, IUpdateViewJsGenerator updateViewJsGenerator, int indentCount)
        => collections.Any()
            ? collections
                .Aggregate(jsBuilder, (sb, collection)
                    => jsBuilder
                        .AddLine(propertyChecker.GenerateBeginCheckJs, parentObjectName, collection.Name, indentCount)
                        .AddLine(GenerateJsUpdateCollection, collection, parentObjectName, updateViewJsGenerator, indentCount + 1)
                        .AddTwoLines(propertyChecker.GenerateEndCheckJs, indentCount))
                .TrimEnd()
            : jsBuilder;

    private StringBuilder GenerateJsUpdateCollection(StringBuilder jsBuilder, FullCollectionObjectName collection, string parentObjectName, IUpdateViewJsGenerator updateViewJsGenerator, int indentCount)
     => collection
            .AssociatedNames
            .Aggregate(jsBuilder, (sb, associatedElement)
                => sb
                    .AddLine(updateCollectionJsCodeGenerator.GenerateJs, parentObjectName, collection.Name, associatedElement, updateViewJsGenerator, indentCount)
                    .AppendLine())
            .TrimEnd();
}