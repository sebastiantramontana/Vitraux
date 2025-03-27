using Vitraux.Modeling.Building.ElementBuilders;
using Vitraux.Modeling.Building.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Data.Values;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling.Building.ElementBuilders;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class ElementPlaceBuildingTest
{
    [Test]
    public void ContentPlaceTest()
    {
        var expectedPlace = TestHelper.CreateContentElementPlace();
        TestPlace(expectedPlace, sut => sut.ToContent);
    }

    [Test]
    public void AttributePlaceTest()
    {
        var expectedPlace = TestHelper.CreateAttributeElementPlace("data-weight");
        TestPlace(expectedPlace, sut => sut.ToAttribute("data-weight"));
    }

    private static void TestPlace(ElementPlace expectedPlace, Func<IRootValueElementPlaceBuilder<ViewModelTest, IRootValueFinallizable<ViewModelTest>>, IRootValueFinallizable<ViewModelTest>> actFunc)
    {
        var valueModel = new ValueModel((ViewModelTest e) => e.Weigth);
        var modelMapper = new ModelMapperRoot<ViewModelTest>();
        var valueModelBuilder = new MapValueBuilder<ViewModelTest>(valueModel, modelMapper);
        var sut = valueModelBuilder
            .ByPopulatingElements.ById("append-to-element-id")
            .FromTemplate("template-id")
            .ToChildren.ByQuery(".children");

        var result = actFunc(sut);

        Assert.That(result, Is.Not.Null);
        TestHelper.AssertPlace(valueModel.TargetElements.Single().Place, expectedPlace, false);
    }
}
