using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;
using Vitraux.Test.Example;

namespace Vitraux.Test.Modeling.Building;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class BuildingValueModelTest
{
    [Test]
    public void AddingNewTargetValueToValueModelTest()
    {
        var selector1 = new ElementIdSelectorString("test-id");
        var selector2 = new ElementQuerySelectorString(".test > p");

        var templeateSelector = new ElementTemplateSelectorString("template-id")
        {
            ElementToAppend = new PopulatingAppendToElementIdSelectorString("element-to-append-id"),
            TargetChildElement = new ElementQuerySelectorString(".chidren")
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
