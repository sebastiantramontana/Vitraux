namespace Vitraux.Execution.Building;

internal class VitrauxBuilder(IEnumerable<IViewModelUpdateFunctionBuilder> viewBuilders) : IVitrauxBuilder
{
    public async Task Build()
    {
        var tasks = viewBuilders.Select(builder => builder.Build()).ToArray();

        await Task.WhenAll(tasks);
    }
}