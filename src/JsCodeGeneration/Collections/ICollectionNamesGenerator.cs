using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionNamesGenerator
{
    IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsObjectName> allJsObjectNames);
}
