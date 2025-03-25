namespace Vitraux.Test.Example;

public record class Vaccine(string Name, DateTime DateApplied, IEnumerable<string> Ingredients);
