using System.Collections;
using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking.Encoded;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelShallowChangesTracker<TViewModel>(
    ISerializablePropertyValueExtractor serializablePropertyValueExtractor,
    IViewModelNoChangesTracker<TViewModel> noChangesTracker,
    IViewModelJsNamesRepositoryGeneric<TViewModel> vmJsNamesRepository)
    : IViewModelShallowChangesTracker<TViewModel>
{
    private readonly Dictionary<string, object?> _previousValues = [];

    public EncodedTrackedViewModelJsAllData Track(object? objToTrack, ViewModelJsNames vmNames)
    {
        if (objToTrack is null)
            return new([], []);

        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private List<EncodedTrackedJsValueData> TrackValues(object objToTrack, IEnumerable<ViewModelJsValueName> valueNames)
    {
        var selectedValues = new List<EncodedTrackedJsValueData>();

        foreach (var valueName in valueNames)
        {
            var valueInfo = serializablePropertyValueExtractor.GetValueInfo(valueName.ValuePropertyValueDelegate, objToTrack);

            if (valueInfo.IsSimpleType)
            {
                CollectEncodedValueByTracking(valueName.ValuePropertyName, valueInfo.Value, () => TrackNewEncodedSimpleValue(valueName.ValuePropertyName, valueInfo.Value), selectedValues);
            }
            else
            {
                CollectEncodedValueByTracking(valueName.ValuePropertyName, valueInfo.Value, () => TrackNewEncodedObjectValue(valueName.ValuePropertyName, valueInfo.Value, valueInfo.ValueType), selectedValues);
            }
        }

        return selectedValues;
    }

    private void CollectEncodedValueByTracking(string propertyName, object? value, Func<EncodedTrackedJsValueData> trackNewEncodedValueFunc, ICollection<EncodedTrackedJsValueData> collector)
    {
        if (TryGetPreviousValue(propertyName, out var previousValue))
        {
            if (!CompareObjects(value, previousValue))
                CollectNewTrackedEncodedValue(trackNewEncodedValueFunc, collector);
        }
        else
        {
            CollectNewTrackedEncodedValue(trackNewEncodedValueFunc, collector);
        }
    }

    private static void CollectNewTrackedEncodedValue(Func<EncodedTrackedJsValueData> trackNewEncodedValueFunc, ICollection<EncodedTrackedJsValueData> collector)
    {
        var encodedValue = trackNewEncodedValueFunc.Invoke();
        collector.Add(encodedValue);
    }

    private EncodedTrackedJsSimpleValueData TrackNewEncodedSimpleValue(string propertyName, object? value)
    {
        TrackNewValue(propertyName, value);
        return CreateEncodedTrackedJsSimpleValueData(propertyName, value);
    }

    private EncodedTrackedJsValueData TrackNewEncodedObjectValue(string propertyName, object? valueToTrack, Type valueType)
    {
        TrackNewValue(propertyName, valueToTrack);

        var allData = TryTrackStoredViewModel(valueToTrack, valueType);

        return (allData is null)
            ? CreateEncodedTrackedJsSimpleValueData(propertyName, valueToTrack)
            : CreateEncodedComplexObjectValue(propertyName, allData);
    }

    private EncodedTrackedViewModelJsAllData? TryTrackStoredViewModel(object? vmToTrack, Type vmType)
    {
        var childrenVMJsNames = GetViewModelJsNames(vmType);

        return childrenVMJsNames is not null
            ? noChangesTracker.Track(vmToTrack, childrenVMJsNames)
            : null;
    }

    private ViewModelJsNames? GetViewModelJsNames(Type valueType)
        => vmJsNamesRepository.GetNamesByViewModelType(valueType);

    //private static ViewModelJsNames CreateViewModelJsNames(string propertyName, object? value)
    //    => new([new(propertyName, () => value)], []);

    private List<EncodedTrackedViewModelJsCollectionData> TrackCollections(object objToTrack, IEnumerable<ViewModelJsCollectionName> collectionNames)
    {
        var selectedCollections = new List<EncodedTrackedViewModelJsCollectionData>();

        foreach (var collectionName in collectionNames)
        {
            var propertyCollection = serializablePropertyValueExtractor.GetCollection(collectionName.CollectionPropertyValueDelegate, objToTrack);

            if (TryGetPreviousValue<IEnumerable>(collectionName.CollectionPropertyName, out var previousCollection))
            {
                if (!CompareCollections(propertyCollection, previousCollection))
                {
                    TrackNewValue(collectionName.CollectionPropertyName, propertyCollection);
                    selectedCollections.Add(CreateEncodedCollection(propertyCollection, collectionName));
                }
            }
            else
            {
                TrackNewValue(collectionName.CollectionPropertyName, propertyCollection);
                selectedCollections.Add(CreateEncodedCollection(propertyCollection, collectionName));
            }
        }

        return selectedCollections;
    }

    private EncodedTrackedJsSimpleValueData CreateEncodedTrackedJsSimpleValueData(string name, object? propertyValue)
    {
        var safePropertyValue = serializablePropertyValueExtractor.GetSafeValue(propertyValue);
        return new(EncodeName(name), safePropertyValue);
    }

    private static EncodedTrackedComplexViewModelJsValueData CreateEncodedComplexObjectValue(string name, EncodedTrackedViewModelJsAllData propertyAllData)
        => new(EncodeName(name), propertyAllData);

    private EncodedTrackedViewModelJsCollectionData CreateEncodedCollection(IEnumerable<object?> childrenToTrack, ViewModelJsCollectionName collectionName)
    {
        var dataChildren = childrenToTrack.SelectMany(ct
            => collectionName.Children.Select(cn => noChangesTracker.Track(ct, cn)));

        return new EncodedTrackedViewModelJsCollectionData(EncodeName(collectionName.CollectionPropertyName), dataChildren);
    }

    private bool TryGetPreviousValue<T>(string name, out T value)
        where T : class
    {
        var exist = TryGetPreviousValue(name, out var previousValue);
        value = (previousValue as T)!;

        return exist;
    }

    private bool TryGetPreviousValue(string name, out object? value)
        => _previousValues.TryGetValue(name, out value);

    private void TrackNewValue(string name, object? value)
        => _previousValues[name] = value;

    private static bool CompareCollections(IEnumerable collection1, IEnumerable collection2)
    {
        if (ReferenceEquals(collection1, collection2))
            return true;

        var enumerator1 = collection1.GetEnumerator();
        var enumerator2 = collection2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            if (!CompareObjects(enumerator1.Current, enumerator2.Current))
                return false;
        }

        return !enumerator1.MoveNext() && !enumerator2.MoveNext();
    }

    private static bool CompareObjects(object? item1, object? item2)
        => (item1 is null && item2 is null) || (item1?.Equals(item2) ?? false);

    private static JsonEncodedText EncodeName(string name)
        => JsonEncodedText.Encode(name);
}