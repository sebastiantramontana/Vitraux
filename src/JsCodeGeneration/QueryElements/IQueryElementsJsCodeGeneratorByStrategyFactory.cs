namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGeneratorByStrategyFactory
{
    IQueryElementsJsCodeGenerator GetInstance(QueryElementStrategy strategy);
}
