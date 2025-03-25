using Vitraux.JsCodeGeneration.QueryElements;

namespace Vitraux;

public record class ConfigurationBehavior(QueryElementStrategy QueryElementStrategy, bool TrackChanges, bool CacheVMFunctions);