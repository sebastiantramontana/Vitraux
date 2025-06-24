using System.Text.Json;

namespace Vitraux.Execution.ViewModelNames;

internal record class ViewModelJsValueName(JsonEncodedText ValuePropertyName, Delegate ValuePropertyValueDelegate);
