namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGeneratorContext
{
    IQueryElementsJsCodeGenerator GetStrategy(QueryElementStrategy strategy);
}
