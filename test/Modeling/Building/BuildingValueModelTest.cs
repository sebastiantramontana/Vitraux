using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Test.Example;

namespace Vitraux.Test.Modeling.Building
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class BuildingValueModelTest
    {
        [Test]
        public void AddingNewTargetValueToValueModelTest()
        {
            var selector1 = new ElementIdSelector("test-id");
            var selector2 = new ElementQuerySelector(".test > p");

            var templeateSelector = new ElementTemplateSelector("template-id")
            {
                ElementToAppend = new PopulatingAppendToElementIdSelector("element-to-append-id"),
                TargetChildElement = new ElementQuerySelector(".chidren")
            };

            var func1 = (ViewModelTest e) => e.Name;

            var actualvalue = TestHelper.CreateValueModel(func1,
            [
                TestHelper.CreateTargetElement(selector1, TestHelper.CreateContentElementPlace()),
                TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-name"))
            ]);

            var expectedValue = TestHelper.CreateValueModel(func1,
            [
                TestHelper.CreateTargetElement(selector1, TestHelper.CreateContentElementPlace()),
                TestHelper.CreateTargetElement(selector2, TestHelper.CreateAttributeElementPlace("data-name")),
                TestHelper.CreateTargetElement(templeateSelector,TestHelper.CreateAttributeElementPlace("attribute"))
            ]);

            var modelMapper = new ModelMapperRoot<PetOwner>();
            var sut = new MapValueBuilder<PetOwner>(actualvalue, modelMapper);

            var result = sut
                .ByPopulatingElements.ById("element-to-append-id")
                .FromTemplate("template-id")
                .ToChildren.ByQuery(".chidren")
                .ToAttribute("attribute");

            Assert.That(result, Is.Not.Null);
            TestHelper.AssertValueModel(actualvalue, expectedValue, true);
        }
    }
}
