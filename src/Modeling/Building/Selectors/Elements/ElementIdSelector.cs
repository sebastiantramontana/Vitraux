namespace Vitraux.Modeling.Building.Selectors.Elements
{
    public record class ElementIdSelector : ElementSelector
    {
        internal ElementIdSelector(string elementId)
            : base(ElementSelection.Id, elementId)
        {
        }
    }
}
