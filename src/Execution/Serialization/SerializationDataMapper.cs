using System.Text.Json;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.Execution.Serialization;

internal class SerializationDataMapper : ISerializationDataMapper
{
    public EncodedViewModelSerializationData MapToEncoded(ViewModelSerializationData viewModelSerializationData)
        => new(MapToEncoded(viewModelSerializationData.ValueProperties), MapToEncoded(viewModelSerializationData.CollectionProperties));

    private static IEnumerable<EncodedValueViewModelSerializationData> MapToEncoded(IEnumerable<ValueViewModelSerializationData> valuesData)
        => valuesData.Select(MapToEncoded);

    private static EncodedValueViewModelSerializationData MapToEncoded(ValueViewModelSerializationData valueData)
        => new(JsonEncodedText.Encode(valueData.ValuePropertyName), valueData.ValuePropertyValueDelegate);

    private IEnumerable<EncodedCollectionViewModelSerializationData> MapToEncoded(IEnumerable<CollectionViewModelSerializationData> collectionsData)
        => collectionsData.Select(MapToEncoded);

    private EncodedCollectionViewModelSerializationData MapToEncoded(CollectionViewModelSerializationData collectionData)
        => new(JsonEncodedText.Encode(collectionData.CollectionPropertyName), collectionData.CollectionPropertyValueDelegate, MapToEncoded(collectionData.Children));

    private IEnumerable<EncodedViewModelSerializationData> MapToEncoded(IEnumerable<ViewModelSerializationData> viewModelSerializationsData)
        => viewModelSerializationsData.Select(MapToEncoded);
}