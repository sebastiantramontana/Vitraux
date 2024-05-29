namespace Vitraux.Modeling.Building.Selectors.Insertion;

internal abstract record class InsertionSelectorBase
{
    protected InsertionSelectorBase(InsertionSelection from)
    {
        From = from;
    }

    internal InsertionSelection From { get; }
}