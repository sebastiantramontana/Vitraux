namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

public record class PopulatingAppendToElementIdSelector : PopulatingAppendToElementSelector
{
    internal PopulatingAppendToElementIdSelector(string id)
        : base(PopulatingAppendToElementSelection.Id, id)
    {
    }
}
