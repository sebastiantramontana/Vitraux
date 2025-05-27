namespace Vitraux.JsCodeGeneration.Collections;

internal record class CollectionObjectName(string Name, IEnumerable<JsCollectionElementObjectPairNames> AssociatedElementNames);
