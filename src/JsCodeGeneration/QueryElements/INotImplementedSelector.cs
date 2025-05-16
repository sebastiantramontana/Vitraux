using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface INotImplementedSelector
{
    T ThrowNotImplementedException<T>(SelectorBase selector);
}
