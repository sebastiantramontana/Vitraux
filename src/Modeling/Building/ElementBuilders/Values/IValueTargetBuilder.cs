using Vitraux.Modeling.Building.CustomJs;

namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IValueTargetBuilder<TViewModel, TValue>
{
    IValueElementSelectorBuilder<TViewModel, TValue> ToElements { get; }
    ICustomJsBuilder<TViewModel, TValue> ToJs(string jsFunction);
    IValueFinallizable<TViewModel, TValue> ToOwnMapping { get; }
}
