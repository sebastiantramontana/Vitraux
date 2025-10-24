using Moq;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;
using Vitraux.Modeling.Data.Actions;
using Vitraux.Test.Example;
using Vitraux.Test.Utils;

namespace Vitraux.Test.JsCodeGeneration;
public class ActionsJsGenerationTest
{
    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart, ExpectedGeneratedJsActionForOnlyOnceAtStart.ExpectedJsCode)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand, ExpectedGeneratedJsActionForOnlyOnceOnDemand.ExpectedJsCode)]
    [InlineData(QueryElementStrategy.Always, ExpectedGeneratedJsActionForAlways.ExpectedJsCode)]

    public void GenerateJsActionTest(QueryElementStrategy queryElementStrategy, string expectedJsCode)
    {
        var rootActionsJsGenerator = RootActionsJsGeneratorFactory.Create();
        var petownerConfig = new PetOwnerConfiguration(new DataUriConverter());
        var actionKeyGenerator = new ActionKeyGenerator(new AtomicAutoNumberGenerator());
        var serviceProvider = MockServiceProvider(actionKeyGenerator);
        var modelMapper = new ModelMapper<PetOwner>(serviceProvider, actionKeyGenerator);

        var data = petownerConfig.ConfigureMapping(modelMapper);
        var jsElementObjectNames = GetJsActionElementObjectNames(data.Actions, RootActionsJsGeneratorFactory.NotImplementedCaseGuard);

        var actualJsCode = rootActionsJsGenerator.GenerateJs("vm_test", data.Actions, jsElementObjectNames, queryElementStrategy);

        Assert.Equal(expectedJsCode, actualJsCode);
    }

    private static IEnumerable<JsElementObjectName> GetJsActionElementObjectNames(IEnumerable<ActionData> actions, INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var jsObjectNamesGenerator = new JsObjectNamesGenerator(new AtomicAutoNumberGenerator());
        var jsElementObjectNamesGenerator = new JsActionElementObjectNamesGenerator(jsObjectNamesGenerator, notImplementedCaseGuard);

        return jsElementObjectNamesGenerator.Generate(string.Empty, actions);
    }

    private static IServiceProvider MockServiceProvider(IActionKeyGenerator actionKeyGenerator)
    {
        var serviceProviderMock = ServiceProviderMock.MockForPetOwner(actionKeyGenerator);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IPetOwnerActionParameterBinder1)))
            .Returns(MockParamBinder1);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IPetOwnerActionParameterBinder2)))
            .Returns(MockParamBinder2);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IActionParametersBinderAsync<PetOwner>)))
            .Returns(MockParamBinder3);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IActionParametersBinder<PetOwner>)))
            .Returns(MockParamBinder4);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IActionParametersBinderAsync<PetOwner>)))
            .Returns(MockParamBinder3);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(PetOwnerActionParameterBinder3)))
            .Returns(ParamBinder3);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(PetOwnerActionParameterBinder4)))
            .Returns(ParamBinder4);

        return serviceProviderMock.Object;
    }

    private static IPetOwnerActionParameterBinder1 MockParamBinder1 { get; } = Mock.Of<IPetOwnerActionParameterBinder1>();
    private static IPetOwnerActionParameterBinder2 MockParamBinder2 { get; } = Mock.Of<IPetOwnerActionParameterBinder2>();
    private static IActionParametersBinderAsync<PetOwner> MockParamBinder3 { get; } = Mock.Of<IActionParametersBinderAsync<PetOwner>>();
    private static IActionParametersBinder<PetOwner> MockParamBinder4 { get; } = Mock.Of<IActionParametersBinder<PetOwner>>();
    private static PetOwnerActionParameterBinder3 ParamBinder3 { get; } = new PetOwnerActionParameterBinder3();
    private static PetOwnerActionParameterBinder4 ParamBinder4 { get; } = new PetOwnerActionParameterBinder4();
}
