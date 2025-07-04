using System.Text.Json;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal class ViewModelNoChangesTracker<TViewModel>(ISerializablePropertyValueExtractor serializablePropertyValueExtractor) : IViewModelNoChangesTracker<TViewModel>
{
    public EncodedTrackedViewModelAllData Track(object objToTrack, ViewModelJsNames vmNames)
    {
        var values = TrackValues(objToTrack, vmNames.ValueProperties);
        var collections = TrackCollections(objToTrack, vmNames.CollectionProperties);

        return new(values, collections);
    }

    private IEnumerable<EncodedTrackedViewModelValueData> TrackValues(object objToTrack, IEnumerable<ViewModelJsValueName> valueNames)
        => valueNames.Select(value =>
        {
            var encodedName = EncodeName(value.ValuePropertyName);
            var propertyValue = serializablePropertyValueExtractor.GetValue(value.ValuePropertyValueDelegate, objToTrack);

            return new EncodedTrackedViewModelValueData(encodedName, propertyValue);
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
