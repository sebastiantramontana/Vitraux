namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal abstract record class InsertElementSelectorBase : SelectorBase
{
    internal ElementQuerySelectorBase TargetChildElementsSelector { get; set; } = default!;
}