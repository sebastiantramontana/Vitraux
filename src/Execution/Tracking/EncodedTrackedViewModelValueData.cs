using System.Text.Json;

namespace Vitraux.Execution.Tracking;

internal record class EncodedTrackedViewModelValueData(JsonEncodedText ValuePropertyName, string PropertyValue);
