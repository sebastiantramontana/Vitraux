namespace Vitraux.Modeling.Data.Selectors.Values.Insertions;

internal abstract record class InsertElementSelectorBase(ElementSelectorBase ElementToAppend)
{
    internal ElementQuerySelectorBase TargetChildElement { get; set; } = default!;
}