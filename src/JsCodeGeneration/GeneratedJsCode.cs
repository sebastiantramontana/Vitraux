using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration;

internal record class GeneratedJsCode(string InitializeViewJs, string UpdateViewJs, IEnumerable<ValueObjectNameWithData> ValueObjects, IEnumerable<CollectionObjectNameWithData> CollectionObjects);
