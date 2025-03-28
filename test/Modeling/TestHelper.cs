using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Elements;
using Vitraux.Modeling.Data.Selectors.Elements.Populating;
using Vitraux.Modeling.Data.Selectors.Insertion;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Test.Modeling;

internal static class TestHelper
{
    public static ValueData CreateValueModel(Delegate valueFunc, IEnumerable<ElementTarget> targetElements)
        => new(valueFunc)
        {
            TargetElements = targetElements.ToArray()
        };

    public static CollectionTableData CreateCollectionTableModel(Delegate collectionFunc, ElementSelectorBase tableSelector, InsertionSelectorBase rowSelector, IEnumerable<ValueData> values, IEnumerable<CollectionData> collectionElements)
        => FillCollectionElementModel(new CollectionTableData(collectionFunc), tableSelector, rowSelector, values, collectionElements);

    public static CollectionData CreateCollectionElementModel(Delegate collectionFunc, ElementSelectorBase elementSelector, InsertionSelectorBase insertionSelector, IEnumerable<ValueData> values, IEnumerable<CollectionData> collectionElements)
        => FillCollectionElementModel(new CollectionData(collectionFunc), elementSelector, insertionSelector, values, collectionElements);

    public static ElementTemplateSelectorString CreateElementTemplateSelectorToId(string templateId, string elementToAppendId, string toChildQuerySelector)
        => new(templateId)
        {
            ElementToAppend = new PopulatingAppendToElementIdSelectorString(elementToAppendId),
            TargetChildElement = new ElementQuerySelectorString(toChildQuerySelector)
        };

    public static ElementTemplateSelectorString CreateElementTemplateSelectorToQuery(string templateId, string elementToAppendQuerySelector, string toChildQuerySelector)
        => new(templateId)
        {
            ElementToAppend = new PopulatingAppendToElementQuerySelectorString(elementToAppendQuerySelector),
            TargetChildElement = new ElementQuerySelectorString(toChildQuerySelector)
        };

    public static ElementUriSelectorUri CreateElementFetchSelectorToId(Uri uri, string elementToAppendId, string toChildQuerySelector)
        => new(uri)
        {
            ElementToAppend = new PopulatingAppendToElementIdSelectorString(elementToAppendId),
            TargetChildElement = new ElementQuerySelectorString(toChildQuerySelector)
        };

    public static ElementUriSelectorUri CreateElementFetchSelectorToQuery(Uri uri, string elementToAppendQuerySelector, string toChildQuerySelector)
        => new(uri)
        {
            ElementToAppend = new PopulatingAppendToElementQuerySelectorString(elementToAppendQuerySelector),
            TargetChildElement = new ElementQuerySelectorString(toChildQuerySelector)
        };

    public static ElementTarget CreateTargetElement(ElementSelectorBase selector, ElementPlace place)
        => new(selector) { Place = place };

    public static ElementPlace CreateAttributeElementPlace(string attribute)
        => new AttributeElementPlace(attribute);

    public static ElementPlace CreateContentElementPlace()
        => new ContentElementPlace();

    public static void AssertCollectionElementModel(CollectionData actualCollection, CollectionData expectedCollection)
    {
        Assert.That(actualCollection, Is.TypeOf(expectedCollection.GetType()));

        AssertDelegate(actualCollection.CollectionFunc, expectedCollection.CollectionFunc);
        AssertSelector(actualCollection.CollectionSelector.ElementSelector, expectedCollection.CollectionSelector.ElementSelector);
        AssertRowSelector(actualCollection.CollectionSelector.InsertionSelector, expectedCollection.CollectionSelector.InsertionSelector);
        AssertModelMappingData(actualCollection.ModelMappingData, expectedCollection.ModelMappingData);
    }

    public static void AssertValueModel(ValueData actualValueModel, ValueData expectedValueModel, bool canPlaceBeNull)
    {
        Assert.Multiple(() =>
        {
            AssertDelegate(actualValueModel.ValueFunc, expectedValueModel.ValueFunc);
            Assert.That(actualValueModel.TargetElements.Count(), Is.EqualTo(expectedValueModel.TargetElements.Count()));

            for (int i = 0; i < expectedValueModel.TargetElements.Count(); i++)
            {
                AssertTargetElement(actualValueModel.TargetElements.ElementAt(i), expectedValueModel.TargetElements.ElementAt(i), canPlaceBeNull);
            }
        });
    }

    public static void AssertTargetElement(ElementTarget actualTargetElement, ElementTarget expectedTargetElement, bool canPlaceBeNull)
    {
        AssertPlace(actualTargetElement.Place, expectedTargetElement.Place, canPlaceBeNull);
        AssertSelector(actualTargetElement.Selector, expectedTargetElement.Selector);
    }

    public static void AssertPlace(ElementPlace actualPlace, ElementPlace expectedPlace, bool canPlaceBeNull)
    {
        if (canPlaceBeNull)
            Assert.That(actualPlace, Is.Null.And.EqualTo(expectedPlace).Or.Not.Null.And.EqualTo(expectedPlace));
        else
            Assert.That(actualPlace, Is.EqualTo(expectedPlace).And.Not.Null);
    }

    public static void AssertSelector(ElementSelectorBase actualSelector, ElementSelectorBase expectedSelector)
    {
        Assert.That(actualSelector, Is.EqualTo(expectedSelector));
    }

    public static void AssertRowSelector(InsertionSelectorBase actualSelector, InsertionSelectorBase expectedSelector)
    {
        Assert.That(actualSelector, Is.EqualTo(expectedSelector));
    }

    public static void AssertModelMappingData(ModelMappingData actualModelMappingData, ModelMappingData expectedModelMappingData)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actualModelMappingData.Values.Count(), Is.EqualTo(expectedModelMappingData.Values.Count()));
            Assert.That(actualModelMappingData.CollectionElements.Count(), Is.EqualTo(expectedModelMappingData.CollectionElements.Count()));

            for (int i = 0; i < actualModelMappingData.Values.Count(); i++)
            {
                AssertValueModel(actualModelMappingData.Values.ElementAt(i), expectedModelMappingData.Values.ElementAt(i), false);
            }

            for (int i = 0; i < actualModelMappingData.CollectionElements.Count(); i++)
            {
                var actual = actualModelMappingData.CollectionElements.ElementAt(i);
                var expected = expectedModelMappingData.CollectionElements.ElementAt(i);

                AssertCollectionElementModel(actual, expected);
            }
        });
    }

    private static void AssertDelegate(Delegate actual, Delegate expected)
    {
        var actualParametersTypes = GetDelegateParameterTypes(actual);
        var expectedParametersTypes = GetDelegateParameterTypes(expected);

        Assert.Multiple(() =>
        {
            Assert.That(actualParametersTypes, Is.EquivalentTo(expectedParametersTypes));
            Assert.That(actual.Method.ReturnType, Is.EqualTo(expected.Method.ReturnType));
        });
    }

    private static IEnumerable<Type> GetDelegateParameterTypes(Delegate @delegate)
        => @delegate.Method.GetParameters().Select(p => p.ParameterType);

    private static TCollectionElementModel FillCollectionElementModel<TCollectionElementModel>(TCollectionElementModel collectionElementModel, ElementSelectorBase elementSelector, InsertionSelectorBase insertionSelector, IEnumerable<ValueData> values, IEnumerable<CollectionData> collectionElements)
        where TCollectionElementModel : CollectionData
    {
        collectionElementModel.CollectionSelector = new(elementSelector)
        {
            InsertionSelector = insertionSelector
        };

        collectionElementModel.ModelMappingData = new ModelMappingDataFake(values, collectionElements);

        return collectionElementModel;
    }
}
