using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelStringValueData(JsonEncodedText ValuePropertyName, string PropertyValue)
    : EncodedTrackedViewModelValueData(ValuePropertyName);
