using System.Text.Json;

namespace Vitraux.Execution.Tracking.Encoded;

internal record class EncodedTrackedJsSimpleValueData(JsonEncodedText ValuePropertyName, object PropertyValue)
    : EncodedTrackedJsValueData(ValuePropertyName);
