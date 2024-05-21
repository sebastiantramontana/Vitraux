using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Models
{
    internal record class TargetElement(ElementSelector Selector)
    {
        internal ElementPlace Place { get; set; } = default!;
    }
}
