using PetOwnerWasm.ViewModel;

namespace PetOwnerWasm.DataAccess;

internal interface IPetOwnerRepository
{
    Task<IEnumerable<PetOwner>> GetAllPetOwners();
    Task<PetOwner> GetPetOwnerById(int id);
}
