namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal abstract record class InsertElementSelectorBase
{
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}