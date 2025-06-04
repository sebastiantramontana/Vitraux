namespace Vitraux;

public record class ConfigurationBehavior(QueryElementStrategy QueryElementStrategy, bool TrackChanges, VMUpdateFunctionCaching VMUpdateFunctionCaching);

public abstract record class VMUpdateFunctionCaching
{
    public static VMUpdateFunctionCaching NoCache { get; } = new VMUpdateFunctionNoCache();
    public static VMUpdateFunctionCaching ByVersion(string version) => new VMUpdateFunctionCacheByVersion(version);
}

public record class VMUpdateFunctionNoCache : VMUpdateFunctionCaching;
public record class VMUpdateFunctionCacheByVersion(string Version) : VMUpdateFunctionCaching;