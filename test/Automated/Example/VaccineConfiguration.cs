﻿namespace Vitraux.Test.Example;

internal class VaccineConfiguration : IModelConfiguration<Vaccine>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceOnDemand,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.NoCache
    };

    public ModelMappingData ConfigureMapping(IModelMapper<Vaccine> modelMapper)
        => modelMapper
            .MapValue(v => v.Name).ToElements.ByQuery(".div-vaccine").ToContent
            .MapValue(v => v.DateApplied).ToElements.ByQuery(".span-vaccine-date").ToContent
            .MapCollection(v => v.Ingredients)
                .ToContainerElements.ByQuery(".ingredients-list").FromTemplate("ingredients-template")
                    .MapValue(i => i).ToElements.ByQuery(".ingredient-item").ToContent
            .EndCollection
            .Data;
}
