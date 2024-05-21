namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

public abstract record class PopulatingAppendToElementSelector
{
    private protected PopulatingAppendToElementSelector(PopulatingAppendToElementSelection selectionBy, string value)
    {
        SelectionBy = selectionBy;
        Value = value;
    }

    internal PopulatingAppendToElementSelection SelectionBy { get; }
    internal string Value { get; }
}
