namespace PetOwnerWasm.ViewModel;

public record class Vaccine(string Name, DateTime DateApplied, IEnumerable<string> Ingredients);
