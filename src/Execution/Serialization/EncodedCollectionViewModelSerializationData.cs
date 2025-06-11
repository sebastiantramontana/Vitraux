using System.Text.Json;

namespace Vitraux.Execution.Serialization;

internal record class EncodedCollectionViewModelSerializationData(JsonEncodedText CollectionPropertyName, Delegate CollectionPropertyValueDelegate, IEnumerable<EncodedViewModelSerializationData> Children);
