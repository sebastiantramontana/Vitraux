﻿using PetOwnerWasm.ViewModel;
using Vitraux;

namespace PetOwnerWasm.ModelConfigurations;
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
            .MapValue(s => s.Frequency).ToElements.ById("petowner-subscription-frequency").ToContent
            .MapValue(s => s.Amount).ToElements.ById("petowner-subscription-amount").ToContent
            .MapValue(s => s.IsDirectDebit).ToElements.ById("petowner-subscription-is-direct-debit").ToContent
            .MapValue(s => GetDescriptivePaymentStatus(s.IsUpToDate)).ToElements.ById("petowner-subscription-is-up-to-date").ToContent
            .MapValue(s=>s.IsUpToDate).ToElements.ById("petowner-subscription-is-up-to-date").ToAttribute("data-is-up-to-date")
            .Data;

    private static string GetDescriptivePaymentStatus(bool isUpToDate)
        => isUpToDate ? "Up to date" : "Overdue";
}
