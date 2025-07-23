using Moq;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Test.Example;
using Vitraux.Test.JsCodeGeneration;

namespace Vitraux.Test.Utils;

internal static class ServiceProviderMock
{
    public static IServiceProvider MockForPetOwner()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(It.IsAny<Type>()))
            .Returns(new Func<Type, object?>(type =>
                type switch
                {
                    var t when t == typeof(IViewModelJsNamesCacheGeneric<Subscription>) => CreateViewModelJsNamesCacheForSubscription(serviceProviderMock.Object),
                    var t when t == typeof(IModelConfiguration<Vaccine>) => new VaccineConfiguration(),
                    var t when t == typeof(IModelMapper<Vaccine>) => new ModelMapper<Vaccine>(serviceProviderMock.Object),
                    _ => null
                }));

        return serviceProviderMock.Object;
    }

    private static ViewModelJsNamesCacheGeneric<Subscription> CreateViewModelJsNamesCacheForSubscription(IServiceProvider serviceProvider)
    {
        var subscriptionConfig = new SubscriptionConfiguration();
        var modelMapper = new ModelMapper<Subscription>(serviceProvider);
        var data = subscriptionConfig.ConfigureMapping(modelMapper);
        var fullObjNames = RootJsGeneratorFactory.JsFullObjectNamesGenerator.Generate(data);
        var serializationDataMapper = new ViewModelJsNamesMapper();
        var vmKeyGenerator = new ViewModelKeyGenerator();

        var viewModelJsNames = serializationDataMapper.MapFromFull(fullObjNames);
        var viewModelKey = vmKeyGenerator.Generate<Subscription>();

        return new ViewModelJsNamesCacheGeneric<Subscription>(serviceProvider)
        {
            ViewModelJsNames = viewModelJsNames,
            ViewModelKey = viewModelKey
        };
    }
}
