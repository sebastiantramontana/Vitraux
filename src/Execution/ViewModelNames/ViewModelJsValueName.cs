using System.Text.Json;

namespace Vitraux.Execution.ViewModelNames;

internal record class ViewModelJsValueName(string ValuePropertyName, Delegate ValuePropertyValueDelegate);
