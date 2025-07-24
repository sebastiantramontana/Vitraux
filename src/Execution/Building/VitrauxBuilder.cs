namespace Vitraux.Execution.Building;

internal class VitrauxBuilder(IEnumerable<IBuilder> builders) : IVitrauxBuilder
{
    public Task Build()
    {
        var tasks = builders.Select(builder => builder.Build());
        return Task.WhenAll(tasks);
    }
}