namespace Vitraux.Test.Example;

public record PetOwner(string Name, string Address, string PhoneNumber, IEnumerable<Pet> pets);
