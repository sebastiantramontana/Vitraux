using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal record class FullCollectionObjectName(string Name, IEnumerable<JsCollectionNames> AssociatedNames, CollectionData AssociatedData)
    : CollectionObjectNameWithElements(Name, AssociatedNames);
