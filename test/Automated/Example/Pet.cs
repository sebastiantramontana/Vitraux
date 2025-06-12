namespace Vitraux.Test.Example;

public record Pet(string Name, byte[] Photo, IEnumerable<Vaccine> Vaccines, IEnumerable<Antiparasitic> Antiparasitics);
