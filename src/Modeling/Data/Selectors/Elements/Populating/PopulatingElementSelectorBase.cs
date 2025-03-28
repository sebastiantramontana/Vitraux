using Vitraux.Modeling.Data.Selectors.Elements;

namespace Vitraux.Modeling.Data.Selectors.Elements.Populating;

internal abstract record class PopulatingElementSelectorBase : ElementSelectorBase
{
    internal PopulatingAppendToElementSelectorBase ElementToAppend { get; set; } = default!;
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}