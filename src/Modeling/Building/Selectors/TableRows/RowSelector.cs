namespace Vitraux.Modeling.Building.Selectors.TableRows
{
    public abstract record class RowSelector
    {
        private protected RowSelector(RowSelection from, string value)
        {
            From = from;
            Value = value;
        }

        internal RowSelection From { get; }
        internal string Value { get; }
    }
}
