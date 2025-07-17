using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelObjectValueData(JsonEncodedText ValuePropertyName, EncodedTrackedViewModelAllData PropertyAllData)
    : EncodedTrackedViewModelValueData(ValuePropertyName);
