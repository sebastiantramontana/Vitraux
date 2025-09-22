using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionNamesGenerator
{
    IEnumerable<FullCollectionObjectName> Generate(IEnumerable<CollectionData> collections, IEnumerable<JsElementObjectName> currentElementJsObjectNames, IJsFullObjectNamesGenerator jsObjectNamesGenerator, int nestingLevel);
}
