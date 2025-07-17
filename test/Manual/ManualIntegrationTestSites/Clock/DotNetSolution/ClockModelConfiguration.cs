using Vitraux;

namespace ClockWasm;

public class ClockModelConfiguration : IModelConfiguration<ClockViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("test 1.2")
    };

    public ModelMappingData ConfigureMapping(IModelMapper<ClockViewModel> modelMapper)
        => modelMapper
            .MapValue(clock => clock.Hours.ToString("D2")).ToElements.ById("hour").ToContent
            .MapValue(clock => clock.Minutes.ToString("D2")).ToElements.ById("minute").ToContent
            .MapValue(clock => clock.Seconds.ToString("D2")).ToElements.ById("second").ToContent
            .MapValue(clock => clock.TenthSeconds.ToString("D1")).ToElements.ById("tenthsecond").ToContent
            .Data;
}
