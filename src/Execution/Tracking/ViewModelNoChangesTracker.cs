using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking.Encoded;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelNoChangesTracker<TViewModel>(
    ISerializablePropertyValueExtractor serializablePropertyValueExtractor,
    IViewModelJsNamesRepositoryGeneric<TViewModel> vmJsNamesRepository) : IViewModelNoChangesTracker<TViewModel>
{
    public EncodedTrackedViewModelJsAllData Track(object? objToTrack, ViewModelJsNames vmNames)
    {
        if (objToTrack is null)
            return new([], []);

        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private IEnumerable<EncodedTrackedJsValueData> TrackValues(object vmToTrack, IEnumerable<ViewModelJsValueName> valueNames)
        => valueNames.Select<ViewModelJsValueName, EncodedTrackedJsValueData>(value =>
        {
            var encodedPropertyName = EncodeName(value.ValuePropertyName);
            var valueInfo = serializablePropertyValueExtractor.GetValueInfo(value.ValuePropertyValueDelegate, vmToTrack);

            if (valueInfo.IsSimpleType)
            {
                return CreateEncodedTrackedJsSimpleValueData(encodedPropertyName, valueInfo.Value);
            }
            else
            {
                var allData = TryTrackStoredViewModel(valueInfo.Value, valueInfo.ValueType);

                return (allData is null)
                     ? CreateEncodedTrackedJsSimpleValueData(encodedPropertyName, valueInfo.Value)
                     : new EncodedTrackedComplexViewModelJsValueData(encodedPropertyName, allData);
            }
        });

    private EncodedTrackedJsSimpleValueData CreateEncodedTrackedJsSimpleValueData(JsonEncodedText propertyName, object? value)
    {
        var propertyValue = serializablePropertyValueExtractor.GetSafeValue(value);
        return new(propertyName, propertyValue);
    }

    private EncodedTrackedViewModelJsAllData? TryTrackStoredViewModel(object? vmToTrack, Type vmType)
    {
        var childrenVMJsNames = vmJsNamesRepository.GetNamesByViewModelType(vmType);

        return childrenVMJsNames is not null
            ? Track(vmToTrack, childrenVMJsNames)
            : null;
    }

    private IEnumerable<EncodedTrackedViewModelJsCollectionData> TrackCollections(object vmToTrack, IEnumerable<ViewModelJsCollectionName> collectionNames)
        => collectionNames.Select(colItem =>
        {
            var encodedName = EncodeName(colItem.CollectionPropertyName);
            var collectionValues = serializablePropertyValueExtractor.GetCollection(colItem.CollectionPropertyValueDelegate, vmToTrack);
            var dataChildren = collectionValues.SelectMany(cv => colItem.Children.Select(c => Track(cv, c)));

            return new EncodedTrackedViewModelJsCollectionData(encodedName, dataChildren);
        });

    private static JsonEncodedText EncodeName(string name)
        => JsonEncodedText.Encode(name);
}
