namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

public record class PopulatingAppendToElementQuerySelector : PopulatingAppendToElementSelector
{
    public PopulatingAppendToElementQuerySelector(string querySelector)
        : base(PopulatingAppendToElementSelection.QuerySelector, querySelector)
    {
    }
}
