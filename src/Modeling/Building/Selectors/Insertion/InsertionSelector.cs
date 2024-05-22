namespace Vitraux.Modeling.Building.Selectors.Insertion;

public abstract record class InsertionSelector
{
    private protected InsertionSelector(InsertionSelection from, string value)
    {
        From = from;
        Value = value;
    }

    internal InsertionSelection From { get; }
    internal string Value { get; }
}
