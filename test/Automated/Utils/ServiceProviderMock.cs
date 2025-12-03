using Moq;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Test.Example;
using Vitraux.Test.JsCodeGeneration;

namespace Vitraux.Test.Utils;

internal static class ServiceProviderMock
{
    public static Mock<IServiceProvider> MockForPetOwner(IActionKeyGenerator actionKeyGenerator)
    {
        var serviceProviderMock = new Mock<IServiceProvider>();

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModelJsNamesRepositoryGeneric<Subscription>)))
            .Returns(() => CreateViewModelJsNamesRepositoryForSubscription(serviceProviderMock.Object, actionKeyGenerator));

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IViewModelConfiguration<Vaccine>)))
            .Returns(new VaccineConfiguration());

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IModelMapper<Vaccine>)))
            .Returns(new ModelMapper<Vaccine>(serviceProviderMock.Object, actionKeyGenerator));

        return serviceProviderMock;
    }

    private static ViewModelJsNamesRepositoryGeneric<Subscription> CreateViewModelJsNamesRepositoryForSubscription(IServiceProvider serviceProvider, IActionKeyGenerator actionKeyGenerator)
    {
        var subscriptionConfig = new SubscriptionConfiguration();
        var modelMapper = new ModelMapper<Subscription>(serviceProvider, actionKeyGenerator);
        var data = subscriptionConfig.ConfigureMapping(modelMapper);
        var fullObjNames = RootJsGeneratorFactory.JsFullObjectNamesGenerator.Generate(data);
        var serializationDataMapper = new ViewModelJsNamesMapper();
        var vmKeyGenerator = new ViewModelKeyGenerator();

        var viewModelJsNames = serializationDataMapper.MapFromFull(fullObjNames);
        var viewModelKey = vmKeyGenerator.Generate<Subscription>();

        return new ViewModelJsNamesRepositoryGeneric<Subscription>(serviceProvider)
        {
            ViewModelJsNames = viewModelJsNames,
            ViewModelKey = viewModelKey
        };
    }
}
