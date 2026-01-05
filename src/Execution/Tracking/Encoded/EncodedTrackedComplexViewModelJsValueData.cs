using System.Text.Json;

namespace Vitraux.Execution.Tracking.Encoded;

internal record class EncodedTrackedComplexViewModelJsValueData(JsonEncodedText ValuePropertyName, EncodedTrackedViewModelJsAllData PropertyAllData)
    : EncodedTrackedJsValueData(ValuePropertyName);
