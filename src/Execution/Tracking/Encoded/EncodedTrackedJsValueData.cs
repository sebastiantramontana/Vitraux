using System.Text.Json;

namespace Vitraux.Execution.Tracking.Encoded;

internal record class EncodedTrackedJsValueData(JsonEncodedText ValuePropertyName);
