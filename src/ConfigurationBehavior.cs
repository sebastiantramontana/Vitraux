﻿namespace Vitraux;

public record class ConfigurationBehavior(QueryElementStrategy QueryElementStrategy, bool TrackChanges, VMUpdateFunctionCaching VMUpdateFunctionCaching);