namespace Vitraux.Test.Example;
internal class SubscriptionConfiguration : IModelConfiguration<Subscription>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("test 1.0")
    };

    public ModelMappingData ConfigureMapping(IModelMapper<Subscription> modelMapper)
        => modelMapper
            .MapValue(s => s.Frequency).ToElements.ById("subscription-frequency").ToContent
            .MapValue(s => s.Amount).ToElements.ById("subscription-amount").ToContent
            .MapValue(s => s.IsUpToDate).ToElements.ById("subscription-isuptodate").ToContent
            .MapValue(s => s.IsDirectDebit).ToElements.ById("subscription-isdirectdebit").ToContent
            .Data;
}
