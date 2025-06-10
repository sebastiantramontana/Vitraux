using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal record class ViewModelSerializationData(IEnumerable<ValueViewModelSerializationData> ValueProperties, IEnumerable<CollectionViewModelSerializationData> CollectionProperties);
