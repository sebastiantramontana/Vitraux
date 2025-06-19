using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal record class FullObjectNames(IEnumerable<FullValueObjectName> ValueNames, IEnumerable<FullCollectionObjectName> CollectionNames);
