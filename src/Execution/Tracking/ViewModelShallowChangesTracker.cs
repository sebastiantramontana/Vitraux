using System.Collections;
using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelShallowChangesTracker<TViewModel>(
    ISerializablePropertyValueExtractor serializablePropertyValueExtractor,
    IViewModelNoChangesTracker<TViewModel> noChangesTracker,
    IViewModelJsNamesCacheGeneric<TViewModel> vmJsNamesCache)
    : IViewModelShallowChangesTracker<TViewModel>
{
    private readonly Dictionary<string, object?> _previousValues = [];

    public EncodedTrackedViewModelAllData Track(object? objToTrack, ViewModelJsNames vmNames)
    {
        if (objToTrack is null)
            return new([], []);

        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private List<EncodedTrackedViewModelValueData> TrackValues(object objToTrack, IEnumerable<ViewModelJsValueName> valueNames)
    {
        var selectedValues = new List<EncodedTrackedViewModelValueData>();

        foreach (var valueName in valueNames)
        {
            var valueInfo = serializablePropertyValueExtractor.GetValueInfo(valueName.ValuePropertyValueDelegate, objToTrack);

            if (valueInfo.IsSimpleType)
            {
                var propertyStringValue = serializablePropertyValueExtractor.GetStringValue(valueInfo.Value);
                CollectEncodedValueByTracking(valueName.ValuePropertyName, propertyStringValue, () => TrackNewEncodedStringValue(valueName.ValuePropertyName, propertyStringValue), selectedValues);
            }
            else
            {
                CollectEncodedValueByTracking(valueName.ValuePropertyName, valueInfo.Value, () => TrackNewEncodedObjectValue(valueName.ValuePropertyName, valueInfo.Value, valueInfo.ValueType), selectedValues);
            }
        }

        return selectedValues;
    }

    private void CollectEncodedValueByTracking(string propertyName, object? value, Func<EncodedTrackedViewModelValueData> trackNewEncodedValueFunc, ICollection<EncodedTrackedViewModelValueData> collector)
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

    private static void CollectNewTrackedEncodedValue(Func<EncodedTrackedViewModelValueData> trackNewEncodedValueFunc, ICollection<EncodedTrackedViewModelValueData> collector)
    {
        var encodedValue = trackNewEncodedValueFunc.Invoke();
        collector.Add(encodedValue);
    }

    private EncodedTrackedViewModelStringValueData TrackNewEncodedStringValue(string propertyName, string stringValue)
    {
        TrackNewValue(propertyName, stringValue);
        return CreateEncodedStringValue(propertyName, stringValue);
    }

    private EncodedTrackedViewModelObjectValueData TrackNewEncodedObjectValue(string propertyName, object? valueToTrack, Type valueType)
    {
        TrackNewValue(propertyName, valueToTrack);

        var childrenVMJsNames = GetViewModelJsNames(valueType);
        var allData = noChangesTracker.Track(valueToTrack, childrenVMJsNames);

        return CreateEncodedObjectValue(propertyName, allData);
    }

    private ViewModelJsNames GetViewModelJsNames(Type valueType)
        => vmJsNamesCache.GetNamesByViewModelType(valueType);

    private List<EncodedTrackedViewModelCollectionData> TrackCollections(object objToTrack, IEnumerable<ViewModelJsCollectionName> collectionNames)
    {
        var selectedCollections = new List<EncodedTrackedViewModelCollectionData>();

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

    private static EncodedTrackedViewModelStringValueData CreateEncodedStringValue(string name, string propertyValue)
        => new(EncodeName(name), propertyValue);

    private static EncodedTrackedViewModelObjectValueData CreateEncodedObjectValue(string name, EncodedTrackedViewModelAllData propertyAllData)
        => new(EncodeName(name), propertyAllData);

    private EncodedTrackedViewModelCollectionData CreateEncodedCollection(IEnumerable<object?> childrenToTrack, ViewModelJsCollectionName collectionName)
    {
        var dataChildren = childrenToTrack.SelectMany(ct
            => collectionName.Children.Select(cn => noChangesTracker.Track(ct, cn)));

        return new EncodedTrackedViewModelCollectionData(EncodeName(collectionName.CollectionPropertyName), dataChildren);
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