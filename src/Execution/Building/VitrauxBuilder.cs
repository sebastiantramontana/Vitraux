namespace Vitraux.Execution.Building;

internal class VitrauxBuilder(IEnumerable<IViewModelUpdateFunctionBuilder> viewBuilders) : IVitrauxBuilder
{
    public Task Build()
    {
        var tasks = viewBuilders.Select(builder => builder.Build());
        return Task.WhenAll(tasks);
    }
}