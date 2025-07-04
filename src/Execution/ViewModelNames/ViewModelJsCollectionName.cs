using System.Text.Json;

namespace Vitraux.Execution.ViewModelNames;

internal record class ViewModelJsCollectionName(string CollectionPropertyName, Delegate CollectionPropertyValueDelegate, IEnumerable<ViewModelJsNames> Children);
