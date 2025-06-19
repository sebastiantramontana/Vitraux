using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsFullObjectNamesGenerator(
    IValueNamesGenerator valueNamesGenerator,
    ICollectionNamesGenerator collectionNamesGenerator) : IJsFullObjectNamesGenerator
{
    public FullObjectNames Generate(ModelMappingData modelMappingData, IEnumerable<JsObjectName> allJsElementObjectNames)
    {
        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values, allJsElementObjectNames);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.Collections, allJsElementObjectNames, this);

        return new(valueNames, collectionNames);
    }
}
