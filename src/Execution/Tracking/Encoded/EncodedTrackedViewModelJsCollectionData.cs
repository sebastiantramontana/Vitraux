using System.Text.Json;

namespace Vitraux.Execution.Tracking.Encoded;

internal record class EncodedTrackedViewModelJsCollectionData(JsonEncodedText ValuePropertyName, IEnumerable<EncodedTrackedViewModelJsAllData> DataChildren);
