using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal record class JsCollectionElementObjectPairNames(string AppendToJsObjectName, string ElementToInsertJsObjectName, CollectionElementTarget Target, FullObjectNames Children)
    : JsCollectionNames;
