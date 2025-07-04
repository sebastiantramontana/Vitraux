using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsFullObjectNamesGenerator(
    IValueNamesGenerator valueNamesGenerator,
    ICollectionNamesGenerator collectionNamesGenerator,
    IJsElementObjectNamesGenerator jsElementObjectNamesGenerator)
    : IJsFullObjectNamesGenerator
{
    public FullObjectNames Generate(ModelMappingData modelMappingData)
       => Generate(string.Empty, modelMappingData, 0);

    public FullObjectNames Generate(string elementNamePrefix, ModelMappingData modelMappingData, int nestingLevel)
    {
        var jsElementObjectNames = jsElementObjectNamesGenerator.Generate(elementNamePrefix, modelMappingData);

        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values, jsElementObjectNames);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.Collections, jsElementObjectNames, this, nestingLevel);

        return new(valueNames, collectionNames, jsElementObjectNames);
    }
}
