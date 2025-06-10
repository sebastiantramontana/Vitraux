using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal record class CollectionViewModelSerializationData(string CollectionPropertyName, Delegate CollectionPropertyValueDelegate, IEnumerable<ViewModelSerializationData> Children);
