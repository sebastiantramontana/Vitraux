﻿using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling
{
    internal static class TestHelper
    {
        public static ValueModel CreateValueModel(Delegate valueFunc, IEnumerable<TargetElement> targetElements)
            => new(valueFunc)
            {
                TargetElements = targetElements.ToArray()
            };

        public static CollectionTableModel CreateCollectionTableModel(Delegate collectionFunc, ElementSelector tableSelector, InsertionSelector rowSelector, IEnumerable<ValueModel> values, IEnumerable<CollectionElementModel> collectionElements)
            => FillCollectionElementModel(new CollectionTableModel(collectionFunc), tableSelector, rowSelector, values, collectionElements);

        public static CollectionElementModel CreateCollectionElementModel(Delegate collectionFunc, ElementSelector elementSelector, InsertionSelector insertionSelector, IEnumerable<ValueModel> values, IEnumerable<CollectionElementModel> collectionElements)
            => FillCollectionElementModel(new CollectionElementModel(collectionFunc), elementSelector, insertionSelector, values, collectionElements);

        public static ElementTemplateSelector CreateElementTemplateSelectorToId(string templateId, string elementToAppendId, string toChildQuerySelector)
            => new(templateId)
            {
                ElementToAppend = new PopulatingAppendToElementIdSelector(elementToAppendId),
                TargetChildElement = new ElementQuerySelector(toChildQuerySelector)
            };

        public static ElementTemplateSelector CreateElementTemplateSelectorToQuery(string templateId, string elementToAppendQuerySelector, string toChildQuerySelector)
            => new(templateId)
            {
                ElementToAppend = new PopulatingAppendToElementQuerySelector(elementToAppendQuerySelector),
                TargetChildElement = new ElementQuerySelector(toChildQuerySelector)
            };

        public static TargetElement CreateTargetElement(ElementSelector selector, ElementPlace place)
            => new(selector) { Place = place };

        public static ElementPlace CreateAttributeElementPlace(string attribute)
            => new AttributeElementPlace(attribute);

        public static ElementPlace CreateContentElementPlace()
            => new ContentElementPlace();

        public static void AssertCollectionElementModel(CollectionElementModel actualCollection, CollectionElementModel expectedCollection)
        {
            Assert.That(actualCollection, Is.TypeOf(expectedCollection.GetType()));

            AssertDelegate(actualCollection.CollectionFunc, expectedCollection.CollectionFunc);
            AssertSelector(actualCollection.ElementSelector, expectedCollection.ElementSelector);
            AssertRowSelector(actualCollection.InsertionSelector, expectedCollection.InsertionSelector);
            AssertModelMappingData(actualCollection.ModelMappingData, expectedCollection.ModelMappingData);
        }

        public static void AssertValueModel(ValueModel actualValueModel, ValueModel expectedValueModel, bool canPlaceBeNull)
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

        public static void AssertTargetElement(TargetElement actualTargetElement, TargetElement expectedTargetElement, bool canPlaceBeNull)
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

        public static void AssertSelector(ElementSelector actualSelector, ElementSelector expectedSelector)
        {
            Assert.That(actualSelector, Is.EqualTo(expectedSelector));
        }

        public static void AssertRowSelector(InsertionSelector actualSelector, InsertionSelector expectedSelector)
        {
            Assert.That(actualSelector, Is.EqualTo(expectedSelector));
        }

        public static void AssertModelMappingData(IModelMappingData actualModelMappingData, IModelMappingData expectedModelMappingData)
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

        private static TCollectionElementModel FillCollectionElementModel<TCollectionElementModel>(TCollectionElementModel collectionElementModel, ElementSelector elementSelector, InsertionSelector insertionSelector, IEnumerable<ValueModel> values, IEnumerable<CollectionElementModel> collectionElements)
            where TCollectionElementModel : CollectionElementModel
        {
            collectionElementModel.ElementSelector = elementSelector;
            collectionElementModel.InsertionSelector = insertionSelector;
            collectionElementModel.ModelMappingData = new ModelMappingDataFake(values, collectionElements);

            return collectionElementModel;
        }
    }
}
