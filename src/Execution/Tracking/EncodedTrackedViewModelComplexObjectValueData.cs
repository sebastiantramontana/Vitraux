using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelComplexObjectValueData(JsonEncodedText ValuePropertyName, EncodedTrackedViewModelAllData PropertyAllData)
    : EncodedTrackedViewModelValueData(ValuePropertyName);
