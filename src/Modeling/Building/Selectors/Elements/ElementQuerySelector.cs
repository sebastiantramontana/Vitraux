namespace Vitraux.Modeling.Building.Selectors.Elements
{
    public record class ElementQuerySelector : ElementSelector
    {
        internal ElementQuerySelector(string querySelector)
            : base(ElementSelection.QuerySelector, querySelector)
        {
        }
    }
}
