using System.Collections;
using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelShallowChangesTracker<TViewModel>(
    ISerializablePropertyValueExtractor serializablePropertyValueExtractor,
    IViewModelNoChangesTracker<TViewModel> noChangesTracker)
    : IViewModelShallowChangesTracker<TViewModel>
{
    private readonly Dictionary<string, object> _previousValues = [];

    public EncodedTrackedViewModelAllData Track(object objToTrack, ViewModelJsNames vmNames)
    {
        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private IEnumerable<EncodedTrackedViewModelValueData> TrackValues(object objToTrack, IEnumerable<ViewModelJsValueName> valueNames)
    {
        var selectedValues = new List<EncodedTrackedViewModelValueData>();

        foreach (var valueName in valueNames)
        {
            var propertyValue = serializablePropertyValueExtractor.GetValue(valueName.ValuePropertyValueDelegate, objToTrack);

            if (TryGetPreviousValue<string>(valueName.ValuePropertyName, out var previousValue))
            {
                if (!propertyValue.Equals(previousValue))
                {
                    TrackNewValue(valueName.ValuePropertyName, propertyValue);
                    selectedValues.Add(CreateEncodedValue(valueName.ValuePropertyName, propertyValue));
                }
            }
            else
            {
                TrackNewValue(valueName.ValuePropertyName, propertyValue);
                selectedValues.Add(CreateEncodedValue(valueName.ValuePropertyName, propertyValue));
            }
        }

        return selectedValues;
    }

    private IEnumerable<EncodedTrackedViewModelCollectionData> TrackCollections(object objToTrack, IEnumerable<ViewModelJsCollectionName> collectionNames)
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
                    selectedCollections.Add(CreateEncodedCollection(objToTrack, collectionName));
                }
            }
            else
            {
                TrackNewValue(collectionName.CollectionPropertyName, propertyCollection);
                selectedCollections.Add(CreateEncodedCollection(objToTrack, collectionName));
            }
        }

        return selectedCollections;
    }

    private static EncodedTrackedViewModelValueData CreateEncodedValue(string name, string propertyValue)
        => new(EncodeName(name), propertyValue);

    private EncodedTrackedViewModelCollectionData CreateEncodedCollection(object objToTrack, ViewModelJsCollectionName collectionName)
    {
        var dataChildren = collectionName.Children.Select(c => noChangesTracker.Track(objToTrack, c));
        return new EncodedTrackedViewModelCollectionData(EncodeName(collectionName.CollectionPropertyName), dataChildren);
    }

    private bool TryGetPreviousValue<T>(string name, out T value)
        where T : class
    {
        var exist = _previousValues.TryGetValue(name, out var previousValue);
        value = (previousValue as T)!;

        return exist;
    }

    private void TrackNewValue(string name, object value)
        => _previousValues[name] = value;

    private static bool CompareCollections(IEnumerable collection1, IEnumerable collection2)
    {
        if (ReferenceEquals(collection1, collection2))
            return true;

        var enumerator1 = collection1.GetEnumerator();
        var enumerator2 = collection2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            if (!CompareCollectionItems(enumerator1.Current, enumerator2.Current))
                return false;
        }

        return !enumerator1.MoveNext() && !enumerator2.MoveNext();
    }

    private static bool CompareCollectionItems(object? item1, object? item2)
    {
        if (item1 is null && item2 is null)
            return true;

        if (item1 is null || item2 is null)
            return false;

        return item1.Equals(item2);
    }

    private static JsonEncodedText EncodeName(string name)
        => JsonEncodedText.Encode(name);
}