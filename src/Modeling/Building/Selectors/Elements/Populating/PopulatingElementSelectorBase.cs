namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal abstract record class PopulatingElementSelectorBase : ElementSelectorBase
{
    protected PopulatingElementSelectorBase(ElementSelection elementSelection)
        : base(elementSelection)
    {
    }

    internal PopulatingAppendToElementSelectorBase ElementToAppend { get; set; } = default!;
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}