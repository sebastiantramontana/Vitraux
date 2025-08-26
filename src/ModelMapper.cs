﻿using System.Diagnostics.CodeAnalysis;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
internal class ModelMapper<TViewModel>(IServiceProvider serviceProvider) : IModelMapper<TViewModel>
{
    public IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func)
    {
        var newValue = new ValueData(func);
        Data.AddValue(newValue);

        return new RootValueTargetBuilder<TViewModel, TValue>(newValue, this);
    }

    public IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func)
    {
        var collection = new CollectionData(func);
        Data.AddCollection(collection);

        return new RootCollectionTargetBuilder<TItem, TViewModel>(collection, this, serviceProvider);
    }

    public IRootActionSourceBuilder<TViewModel> MapAction(Func<TViewModel, IDictionary<string, IEnumerable<string>>, Task> func)
    {
        var action = new ActionData(func);
        Data.AddAction(action);

        return new RootActionSourceBuilder<TViewModel>(action, this);
    }

    public ModelMappingData Data { get; } = new ModelMappingData();
}

