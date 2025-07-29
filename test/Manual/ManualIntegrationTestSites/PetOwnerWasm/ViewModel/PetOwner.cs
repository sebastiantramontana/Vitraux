namespace PetOwnerWasm.ViewModel;

public record class PetOwner(int Id, string Name, string Address, string? PhoneNumber, string HtmlComments, Subscription Subscription, IEnumerable<Pet> Pets);