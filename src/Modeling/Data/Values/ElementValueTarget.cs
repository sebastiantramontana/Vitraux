using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.Modeling.Data.Values;

internal record class ElementValueTarget : IValueTarget
{
    internal ElementSelectorBase Selector { get; set; } = default!;
    internal ElementPlace Place { get; set; } = default!;
    internal InsertElementSelectorBase? Insertion { get; set; }
}
