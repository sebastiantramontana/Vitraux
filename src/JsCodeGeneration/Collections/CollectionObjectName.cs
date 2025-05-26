using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal record class CollectionObjectName(string Name, IEnumerable<JsCollectionElementObjectPairNames> AssociatedElementNames);

internal record class JsCollectionElementObjectPairNames(string AppendToJsObjectName, string ElementToInsertJsObjectName, CollectionElementTarget Target);
