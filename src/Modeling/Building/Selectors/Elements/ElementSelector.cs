namespace Vitraux.Modeling.Building.Selectors.Elements
{
    public abstract record class ElementSelector
    {
        private protected ElementSelector(ElementSelection selectionBy, string value)
        {
            SelectionBy = selectionBy;
            Value = value;
        }

        internal ElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
