namespace Vitraux.Modeling.Models;

internal record class ValueModel(Delegate ValueFunc)
{
    public IEnumerable<TargetElement> TargetElements { get; set; } = [];
}
