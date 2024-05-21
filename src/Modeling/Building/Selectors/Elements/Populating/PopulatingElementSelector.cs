namespace Vitraux.Modeling.Building.Selectors.Elements.Populating;

internal record class PopulatingElementSelector : ElementSelector
{
    internal PopulatingElementSelector(ElementSelection elementSelection, string value)
        : base(elementSelection, value)
    {
    }

    public PopulatingAppendToElementSelector ElementToAppend { get; set; } = default!;
    public ElementQuerySelector TargetChildElement { get; set; } = default!;
}
