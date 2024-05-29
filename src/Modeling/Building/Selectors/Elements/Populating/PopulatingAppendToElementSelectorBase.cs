namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal abstract record class PopulatingAppendToElementSelectorBase
{
    protected PopulatingAppendToElementSelectorBase(PopulatingAppendToElementSelection selectionBy)
    {
        SelectionBy = selectionBy;
    }

    internal PopulatingAppendToElementSelection SelectionBy { get; }
}