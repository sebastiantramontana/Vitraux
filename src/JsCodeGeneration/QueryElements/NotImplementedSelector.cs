using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class NotImplementedSelector : INotImplementedSelector
{
    public T ThrowNotImplementedException<T>(SelectorBase selector)
        => throw new NotImplementedException($"Selector type {selector.GetType().Name} not implemented. See StackTrace.");
}