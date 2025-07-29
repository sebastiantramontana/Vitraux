using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PetOwnerWasm.ModelConfigurations;
using PetOwnerWasm.ViewModel;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Vitraux;

namespace PetOwnerWasm;

[SupportedOSPlatform("browser")]
public partial class Program
{
    private static IViewlUpdater<PetOwner> _petownerViewlUpdater = default!;
    private static IViewlUpdater<AllPetOwnerNames> _allPetownerNamesViewlUpdater = default!;

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        _ = builder.Services
            .AddVitraux()
            .AddConfiguration(() => new VitrauxConfiguration { UseShadowDom = true })
            .AddModelConfiguration<AllPetOwnerNames, AllPetOwnerNamesConfiguration>()
            .AddModelConfiguration<PetOwner, PetOwnerConfiguration>()
            .AddModelConfiguration<Subscription, SubscriptionConfiguration>()
            .AddModelConfiguration<Vaccine, VaccineConfiguration>();

        await using var host = builder.Build();

        try
        {
            await host.Services.BuildVitraux();

            _petownerViewlUpdater = host.Services.GetRequiredService<IViewlUpdater<PetOwner>>();
            _allPetownerNamesViewlUpdater = host.Services.GetRequiredService<IViewlUpdater<AllPetOwnerNames>>();
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
        var petOwners = await GetAllPetOwners();
        var petOwnerNames = petOwners.Select(p => new PetOwnerName(p.Id, p.Name));

        await _allPetownerNamesViewlUpdater.Update(new AllPetOwnerNames(petOwnerNames));
    }

    [JSExport]
    public static async Task SelectPetOwner(int id)
    {
        try
        {
            var petOwners = await GetAllPetOwners();
            var petOwner = petOwners.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"Invalid petowner id: {id}");

            await _petownerViewlUpdater.Update(petOwner);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ex = {ex}");
        }
    }

    private static IEnumerable<PetOwner> _allPetOwners = [];

    private static async Task<IEnumerable<PetOwner>> GetAllPetOwners()
    {
        if (!_allPetOwners.Any())
        {
            _allPetOwners = [
                new PetOwner(1, "John Smith", "123 Evergreen Terrace, Springfield", "+1 (555) 123-4567", CreateHtmlContent("John Smith"), new Subscription(SubscriptionFrequency.Monthly, 9.99, true, true), await GetPets(1)),
                new PetOwner(2, "Maria Gonzalez", "456 Evergreen Terrace, Springfield", "+1 (555) 321-7654", CreateHtmlContent("Maria Gonzalez"), new Subscription(SubscriptionFrequency.Yearly, 99.89, false, true), await GetPets(2)),
                new PetOwner(3, "Charles Wilson", "789 Evergreen Terrace, Springfield", null, CreateHtmlContent("Charles Wilson"), new Subscription(SubscriptionFrequency.Semiannual, 55.67, true, false), await GetPets(3)),
            ];
        }

        return _allPetOwners;
    }

    private static string CreateHtmlContent(string petownerName)
        => $"""
            <h2 style='width: 100%; text-align: center; color: lightblue; font-weight: bold;'>Some comments for {petownerName}</h2>
                <div style='margin-left: 10px;'>
                    <span style='color: red'>bla</span>
                    <span style='color: orange'>bla</span>
                    <span style='color: gold;'>bla</span>
                    <span style='color: green'>bla</span>
                    <span style='color: blue'>bla...&nbsp;</span>
                    <span style='color: purple'>{petownerName}</span>
                </div>
            """;
    private static async Task<IEnumerable<Pet>> GetPets(int petOwnerId)
    {
        var petPhotos = await GetAllPetPhotos();

        return petOwnerId switch
        {
            1 => [
                    new Pet("Boby", petPhotos.ElementAt(0), GetVaccines(petOwnerId,'A',2), GetAntiparasitics(petOwnerId,'A',3)),
                    new Pet("Pelusa", petPhotos.ElementAt(1), GetVaccines(petOwnerId,'B',3), GetAntiparasitics(petOwnerId,'B',2)),
                    new Pet("Max", petPhotos.ElementAt(2), GetVaccines(petOwnerId,'C',1), GetAntiparasitics(petOwnerId,'C',1)),
                    new Pet("Rocky", petPhotos.ElementAt(3), GetVaccines(petOwnerId,'D',2), GetAntiparasitics(petOwnerId,'D',2)),
                ],
            2 => [
                    new Pet("Lola", petPhotos.ElementAt(4), GetVaccines(petOwnerId,'E',3), GetAntiparasitics(petOwnerId,'E',3)),
                ],
            3 => [
                    new Pet("Caty", petPhotos.ElementAt(5), GetVaccines(petOwnerId, 'F', 4), GetAntiparasitics(petOwnerId, 'F', 2)),
                    new Pet("Toto", petPhotos.ElementAt(6), GetVaccines(petOwnerId, 'G', 1), GetAntiparasitics(petOwnerId, 'G', 1)),
                ],
            _ => throw new NotImplementedException($"No pet photos implemented for petowner with id = {petOwnerId}")
        };
    }

    private static IEnumerable<Vaccine> GetVaccines(int petOwnerId, char letter, int count)
        => Enumerable.Range(petOwnerId, count).Select(n => new Vaccine($"Polyvalent {n}{letter}", new DateTime(2025, count, n + 2), GetIngredients(petOwnerId, letter, count)));

    private static IEnumerable<string> GetIngredients(int petOwnerId, char letter, int count)
        => Enumerable.Range(petOwnerId, count).Select(n => $"Bordetella {n}{letter}");

    private static IEnumerable<Antiparasitic> GetAntiparasitics(int petOwnerId, char letter, int count)
        => Enumerable.Range(petOwnerId, count).Select(n => new Antiparasitic($"Bravecto {n}{letter}", new DateTime(2025, count, n + 7)));

    private static IEnumerable<byte[]> _preloadedAllPetPhotos = [];
    private static async Task<IEnumerable<byte[]>> GetAllPetPhotos()
    {
        if (!_preloadedAllPetPhotos.Any())
            _preloadedAllPetPhotos = await DownloadAllPetPhotos();

        return _preloadedAllPetPhotos;
    }

    private static async Task<IEnumerable<byte[]>> DownloadAllPetPhotos()
    {
        const string PhotoUri = "https://placedog.net/100/100?id=";
        IEnumerable<int> photoIds = [3, 5, 8, 9, 10, 14, 16];
        var photos = new List<byte[]>();
        using var httpClient = new HttpClient();

        foreach (var id in photoIds)
            photos.Add(await httpClient.GetByteArrayAsync(PhotoUri + id));

        return photos;
    }
}
