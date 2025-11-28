using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelNoChangesTracker<TViewModel>(
    ISerializablePropertyValueExtractor serializablePropertyValueExtractor,
    IViewModelJsNamesRepositoryGeneric<TViewModel> vmJsNamesRepository) : IViewModelNoChangesTracker<TViewModel>
{
    public EncodedTrackedViewModelAllData Track(object? objToTrack, ViewModelJsNames vmNames)
    {
        if (objToTrack is null)
            return new([], []);

        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private IEnumerable<EncodedTrackedViewModelValueData> TrackValues(object objToTrack, IEnumerable<ViewModelJsValueName> valueNames)
        => valueNames.Select<ViewModelJsValueName, EncodedTrackedViewModelValueData>(value =>
        {
            var encodedName = EncodeName(value.ValuePropertyName);
            var valueInfo = serializablePropertyValueExtractor.GetValueInfo(value.ValuePropertyValueDelegate, objToTrack);

            if (valueInfo.IsSimpleType)
            {
                var propertyValue = serializablePropertyValueExtractor.GetSafeValue(valueInfo.Value);
                return new EncodedTrackedViewModelSimpleValueData(encodedName, propertyValue);
            }
            else
            {
                var childrenVMJsNames = vmJsNamesRepository.GetNamesByViewModelType(valueInfo.ValueType);
                var allData = Track(valueInfo.Value, childrenVMJsNames);
                return new EncodedTrackedViewModelComplexObjectValueData(encodedName, allData);
            }
        });

    private IEnumerable<EncodedTrackedViewModelCollectionData> TrackCollections(object objToTrack, IEnumerable<ViewModelJsCollectionName> collectionNames)
        => collectionNames.Select(colItem =>
        {
            var encodedName = EncodeName(colItem.CollectionPropertyName);
            var collectionValues = serializablePropertyValueExtractor.GetCollection(colItem.CollectionPropertyValueDelegate, objToTrack);
            var dataChildren = collectionValues.SelectMany(cv => colItem.Children.Select(c => Track(cv, c)));

            return new EncodedTrackedViewModelCollectionData(encodedName, dataChildren);
        });

    private static JsonEncodedText EncodeName(string name)
        => JsonEncodedText.Encode(name);
}
