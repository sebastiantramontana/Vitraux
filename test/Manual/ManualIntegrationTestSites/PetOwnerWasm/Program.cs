using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PetOwnerWasm.DataAccess;
using PetOwnerWasm.ModelConfigurations;
using PetOwnerWasm.ViewModel;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Vitraux;

namespace PetOwnerWasm;

[SupportedOSPlatform("browser")]
public partial class Program
{
    private static IViewUpdater<PetOwner> _petownerViewlUpdater = default!;
    private static IViewUpdater<AllPetOwnerNames> _allPetownerNamesViewlUpdater = default!;
    private static IPetOwnerRepository _petOwnerRepository = default!;
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddSingleton<IPetOwnerRepository, PetOwnerRepository>();

        _ = builder.Services
            .AddVitraux()
            .AddConfiguration(() => new VitrauxConfiguration { UseShadowDom = true })
            .AddViewModelConfiguration<AllPetOwnerNames, AllPetOwnerNamesConfiguration>()
                .AddActionParameterBinderAsync<SelectPetOwnerBinder>()
            .AddViewModelConfiguration<PetOwner, PetOwnerConfiguration>()
            .AddViewModelConfiguration<Subscription, SubscriptionConfiguration>()
            .AddViewModelConfiguration<Vaccine, VaccineConfiguration>();

        await using var host = builder.Build();

        try
        {
            _petOwnerRepository = host.Services.GetRequiredService<IPetOwnerRepository>();

            await host.Services.BuildVitraux();

            _petownerViewlUpdater = host.Services.GetRequiredService<IViewUpdater<PetOwner>>();
            _allPetownerNamesViewlUpdater = host.Services.GetRequiredService<IViewUpdater<AllPetOwnerNames>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        await host.RunAsync();
    }

    [JSExport]
    public static async Task RefreshPetOwnerNames()
    {
        while(_petOwnerRepository is null || _allPetownerNamesViewlUpdater is null)
        {
            await Task.Delay(1);
        }

        var petOwners = await _petOwnerRepository.GetAllPetOwners();
        var petOwnerNames = petOwners.Select(p => new PetOwnerName(p.Id, p.Name));

        await _allPetownerNamesViewlUpdater.Update(new AllPetOwnerNames(petOwnerNames, _petOwnerRepository, _petownerViewlUpdater));
    }
}
