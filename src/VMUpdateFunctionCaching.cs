namespace Vitraux;

public abstract record class VMUpdateFunctionCaching
{
    public static VMUpdateFunctionCaching NoCache { get; } = new VMUpdateFunctionNoCache();
    public static VMUpdateFunctionCaching ByVersion(string version) => new VMUpdateFunctionCacheByVersion(version);
}
