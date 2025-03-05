namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal abstract record class PopulatingElementSelectorBase(ElementSelection SelectionBy) : ElementSelectorBase(SelectionBy)
{
    internal PopulatingAppendToElementSelectorBase ElementToAppend { get; set; } = default!;
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}