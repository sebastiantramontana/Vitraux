namespace Vitraux;

public record class ConfigurationBehavior
{
    public QueryElementStrategy QueryElementStrategy { get; init; } = QueryElementStrategy.OnlyOnceAtStart;
    public bool TrackChanges { get; init; } = false;
    public VMUpdateFunctionCaching VMUpdateFunctionCaching { get; init; } = VMUpdateFunctionCaching.NoCache;
    public ActionRegistrationStrategy ActionRegistrationStrategy { get; init; } = ActionRegistrationStrategy.OnlyOnceAtStart;
}