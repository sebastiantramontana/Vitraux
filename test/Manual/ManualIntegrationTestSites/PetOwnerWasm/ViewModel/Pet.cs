namespace PetOwnerWasm.ViewModel;

public record Pet(string Name, byte[] Photo, IEnumerable<Vaccine> Vaccines, IEnumerable<Antiparasitic> Antiparasitics);
