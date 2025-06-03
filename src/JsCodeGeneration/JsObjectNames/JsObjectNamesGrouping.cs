using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal record class JsObjectNamesGrouping(IEnumerable<JsObjectName> AllJsElementObjectNames, IEnumerable<FullValueObjectName> ValueNames, IEnumerable<FullCollectionObjectName> CollectionNames);
