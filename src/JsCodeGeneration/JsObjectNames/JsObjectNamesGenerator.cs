using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsObjectNamesGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IJsElementObjectNamesGenerator jsElementObjectNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    ICollectionNamesGenerator collectionNamesGenerator) : IJsObjectNamesGenerator
{
    public JsObjectNamesGrouping Generate(ModelMappingData modelMappingData, string elementNamePrefix)
    {
        var allJsElementObjectNames = GenerateJsElementObjectNames(modelMappingData, elementNamePrefix);
        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values, allJsElementObjectNames);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.Collections, allJsElementObjectNames);

        return new(allJsElementObjectNames, valueNames, collectionNames);
    }

    private IEnumerable<JsObjectName> GenerateJsElementObjectNames(ModelMappingData modelMappingData, string elementNamePrefix)
    {
        var selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);
        return jsElementObjectNamesGenerator.Generate(elementNamePrefix, selectors);
    }
}
