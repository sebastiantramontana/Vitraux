using System.Text.Json;

namespace Vitraux.Execution.Serialization;

internal record class EncodedValueViewModelSerializationData(JsonEncodedText ValuePropertyName, Delegate ValuePropertyValueDelegate);
