using Microsoft.Playwright;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ModelMapperTest
    {
        [Test]
        public void BuildModelValuesTest()
        {
            ModelMapperRoot<ViewModelTest> sut = new();

            var func1 = (ViewModelTest e) => e.Name;
            var func2 = (ViewModelTest e) => e.Age;

            var result1 = sut
                .MapValue(func1)
                    .ToElements.ById("test-id").ToContent
                    .ToElements.ByQuery(".test > p").ToAttribute("data-name")
                    .ByPopulatingElements.ById("element-to-append-id")
                        .FromTemplate("template-id")
                        .ToChildren.ByQuery(".children")
                        .ToContent;

            var result2 = sut
                .MapValue(func2)
                    .ToElements.ByQuery(".test > p").ToAttribute("data-age");

            //Assert

            var querySelector = new ElementQuerySelectorString(".test > p");
            var templateSelector = new ElementTemplateSelectorString("template-id")
            {
                ElementToAppend = new PopulatingAppendToElementIdSelectorString("element-to-append-id"),
                TargetChildElement = new ElementQuerySelectorString(".children")
            };

            var expectedValue1 = TestHelper.CreateValueModel(func1,
                [
                    TestHelper.CreateTargetElement(new ElementIdSelectorString("test-id"), TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(querySelector, TestHelper.CreateAttributeElementPlace("data-name")),
                    TestHelper.CreateTargetElement(templateSelector, TestHelper.CreateContentElementPlace())
                ]);

            var expectedValue2 = TestHelper.CreateValueModel(func2,
                [
                    TestHelper.CreateTargetElement(querySelector, TestHelper.CreateAttributeElementPlace("data-age"))
                ]);

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);

                var modelMappingData = sut as ModelMappingData;

                Assert.That(modelMappingData.Values.Count(), Is.EqualTo(2));
                TestHelper.AssertValueModel(modelMappingData.Values.ElementAt(0), expectedValue1, false);
                TestHelper.AssertValueModel(modelMappingData.Values.ElementAt(1), expectedValue2, false);
            });
        }
    }
}
