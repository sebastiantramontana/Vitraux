using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelSimpleValueData(JsonEncodedText ValuePropertyName, object PropertyValue)
    : EncodedTrackedViewModelValueData(ValuePropertyName);
