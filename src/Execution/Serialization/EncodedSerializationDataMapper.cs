using System.Text.Json;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.Execution.Serialization;

internal class EncodedSerializationDataMapper : IEncodedSerializationDataMapper
{
    public EncodedViewModelSerializationData MapToEncoded(FullObjectNames fullObjectNames)
    {
        var values = MapValues(fullObjectNames.ValueNames);
        var collections = MapCollections(fullObjectNames.CollectionNames);

        return new EncodedViewModelSerializationData(values, collections);
    }

    private static IEnumerable<EncodedValueViewModelSerializationData> MapValues(IEnumerable<FullValueObjectName> valueNames)
        => valueNames.Select(value => new EncodedValueViewModelSerializationData(JsonEncodedText.Encode(value.Name), value.AssociatedData.DataFunc));

    private IEnumerable<EncodedCollectionViewModelSerializationData> MapCollections(IEnumerable<FullCollectionObjectName> collectionNames)
        => collectionNames.Select(colItem =>
        {
            var viewModelSerializationsDataChildren = colItem.AssociatedElementNames.Select(e => MapToEncoded(e.Children));
            return new EncodedCollectionViewModelSerializationData(JsonEncodedText.Encode(colItem.Name), colItem.AssociatedData.DataFunc, viewModelSerializationsDataChildren);
        });
}