namespace Vitraux.JsCodeGeneration.Initialization;

internal interface IInitializeJsGeneratorContext
{
    IInitializeJsGenerator GetStrategy(QueryElementStrategy strategy);
}
