namespace Vitraux.Modeling.Building.Selectors.Elements;

internal abstract record class ElementSelectorBase
{
    protected ElementSelectorBase(ElementSelection selectionBy)
    {
        SelectionBy = selectionBy;
    }

    internal ElementSelection SelectionBy { get; }
}
