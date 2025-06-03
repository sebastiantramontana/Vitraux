namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator(IJsGenerator jsGenerator) : IRootJsGenerator
{
    public GeneratedJsCode GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy)
        => jsGenerator.GenerateJs(modelMappingData, queryElementStrategy, "vm", "document", string.Empty);
}
