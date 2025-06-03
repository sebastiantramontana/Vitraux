using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal record class FullCollectionObjectName(string Name, IEnumerable<JsCollectionElementObjectPairNames> AssociatedElementNames, CollectionData AssociatedData)
    : CollectionObjectNameWithElements(Name, AssociatedElementNames);
