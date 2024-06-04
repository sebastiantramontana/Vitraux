namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGeneratorByStrategyContext
{
    IQueryElementsJsCodeGenerator GetStrategy(QueryElementStrategy strategy);
}
