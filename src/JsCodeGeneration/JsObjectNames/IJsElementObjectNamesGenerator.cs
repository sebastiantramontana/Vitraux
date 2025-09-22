namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsElementObjectNamesGenerator
{
    public IEnumerable<JsElementObjectName> Generate(string namePrefix, ModelMappingData modelMappingData);
}
