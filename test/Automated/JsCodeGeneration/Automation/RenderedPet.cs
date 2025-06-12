namespace Vitraux.Test.JsCodeGeneration.Automation;

internal record class RenderedPet(string Name, IEnumerable<string?> AnchorCellPetNames, string? Photo, IEnumerable<RenderedVaccine> Vaccines, IEnumerable<RenderedAntiparasitic> Antiparasitics);
