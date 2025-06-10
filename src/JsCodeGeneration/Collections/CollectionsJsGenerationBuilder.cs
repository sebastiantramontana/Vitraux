using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public BuiltCollectionJs BuildJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collections, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var viewModelSerializationsData = new List<CollectionViewModelSerializationData>();

        var jsCode = collections
                        .Aggregate(new StringBuilder(), (sb, collection) =>
                        {
                            var viewModelSerializationsDataChildren = new List<ViewModelSerializationData>();

                            var jsCollection = collection
                                    .AssociatedElementNames
                                    .Aggregate(sb, (sb2, associatedElement) =>
                                    {
                                        var jsCodeCollection = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, updateViewJsGenerator);
                                        var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, jsCodeCollection.JsCode);

                                        viewModelSerializationsDataChildren.Add(jsCodeCollection.ViewModelSerializationData);

                                        return sb2
                                            .AppendLine(propertyCheckerJsCode)
                                            .AppendLine();
                                    });

                            viewModelSerializationsData.Add(new CollectionViewModelSerializationData(collection.Name, collection.AssociatedData.DataFunc, viewModelSerializationsDataChildren));

                            return jsCollection;
                        })
                        .ToString()
                        .TrimEnd();

        return new(jsCode, viewModelSerializationsData);
    }
}