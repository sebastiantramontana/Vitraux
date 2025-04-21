namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal abstract record class InsertElementSelectorBase : SelectorBase
{
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}