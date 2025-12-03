using PetOwnerWasm.DataAccess;
using Vitraux;

namespace PetOwnerWasm.ViewModel;

internal class AllPetOwnerNames(IEnumerable<PetOwnerName> names, IPetOwnerRepository petOwnerRepository, IViewUpdater<PetOwner> petownerViewUpdater)
{
    public IEnumerable<PetOwnerName> Names { get; } = names;

    public async Task SelectPetOwner(int petOwnerId)
    {
        var petOwner = await petOwnerRepository.GetPetOwnerById(petOwnerId);
        await petownerViewUpdater.Update(petOwner);
    }

    public Task AddFakePetOwner()
    {
        var petOwner = new PetOwner(111, "Fake name", "Fake address", "Fake phone", "<h2>Fake Fake Fake Fake Fake</h2>", new Subscription(SubscriptionFrequency.Monthly, -1.0, false, false), []);
        return petownerViewUpdater.Update(petOwner);
    }
}
