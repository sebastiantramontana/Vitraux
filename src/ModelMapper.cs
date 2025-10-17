using System.Diagnostics.CodeAnalysis;
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
internal class ModelMapper<TViewModel>(IServiceProvider serviceProvider, IActionKeyGenerator actionKeyGenerator) : IModelMapper<TViewModel>
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

    public IRootActionSourceBuilder<TViewModel> MapActionAsync(Func<TViewModel, Task> func)
        => AddAction(func);

    public IRootActionSourceBuilder<TViewModel> MapAction(Action<TViewModel> func)
        => AddAction(func);

    public IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinderAsync<TViewModel>
        => AddParametrizableAction<TActionParametersBinder>((binder, actionKey) => new ActionData(binder.BindActionAsync, actionKey));

    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction<TActionParametersBinder>() where TActionParametersBinder : class, IActionParametersBinder<TViewModel>
        => AddParametrizableAction<TActionParametersBinder>((binder, actionKey) => new ActionData(binder.BindAction, actionKey));

    public IRootParametrizableActionSourceBuilder<TViewModel> MapActionAsync()
        => AddParametrizableAction<IActionParametersBinderAsync<TViewModel>>((binder, actionKey) => new ActionData(binder.BindActionAsync, actionKey));

    public IRootParametrizableActionSourceBuilder<TViewModel> MapAction()
        => AddParametrizableAction<IActionParametersBinder<TViewModel>>((binder, actionKey) => new ActionData(binder.BindAction, actionKey));

    public ModelMappingData Data { get; } = new ModelMappingData();

    private RootActionSourceBuilder<TViewModel> AddAction(Delegate func)
    {
        var action = new ActionData(func, GenerateActionKey());
        Data.AddAction(action);

        return new RootActionSourceBuilder<TViewModel>(action, this);
    }

    private RootParametrizableActionSourceBuilder<TViewModel> AddParametrizableAction<TActionParametersBinder>(Func<TActionParametersBinder, string, ActionData> actionDataFactory) where TActionParametersBinder : class
    {
        var binder = GetActionParameterBinder<TActionParametersBinder>();
        var actionKey = GenerateActionKey();
        var action = actionDataFactory.Invoke(binder, actionKey);

        Data.AddAction(action);

        return new RootParametrizableActionSourceBuilder<TViewModel>(action, this);
    }

    private string GenerateActionKey()
        => actionKeyGenerator.Generate();

    private TActionParametersBinder GetActionParameterBinder<TActionParametersBinder>() where TActionParametersBinder : class
        => serviceProvider.GetService(typeof(TActionParametersBinder)) as TActionParametersBinder
            ?? throw new InvalidOperationException($"Action parameter binder '{typeof(TActionParametersBinder).Name}' not found. Add it calling to AddActionParameterBinder<...>() method after AddModelConfiguration<...>()");
}