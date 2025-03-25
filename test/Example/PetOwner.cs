namespace Vitraux.Test.Example;

public record class PetOwner(string Name, string Address, string PhoneNumber, Subscription Subscription, IEnumerable<Pet> Pets);