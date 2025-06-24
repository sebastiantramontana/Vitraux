using System.Collections;
using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelCollectionData(JsonEncodedText ValuePropertyName, IEnumerable PropertyCollectionValue, IEnumerable<EncodedTrackedViewModelAllData> DataChildren);
