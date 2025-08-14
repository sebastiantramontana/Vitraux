<h1 align="center">Vitraux</h1>
<h4 align="center">Mapea tus viewmodels .NET a HTML en WebAssembly.</h4>

<p align="center">
<img src="https://github.com/sebastiantramontana/Vitraux/blob/main/assets/vitraux-banner.png" />
</p>

## Introducción
Vitraux es una librería para .NET que permite manipular el DOM de HTML en aplicaciones WebAssembly. Con un enfoque declarativo, mapea ViewModels de .Net directamente a elementos HTML estándar, manteniendo la separación de responsabilidades y el desacople entre frontend y backend, y facilitando el mantenimiento, la legibilidad y la escalabilidad del código.

![Mapping](https://github.com/sebastiantramontana/Vitraux/blob/main/assets/readme/mapping-banner-final.png)

---

## Principales ventajas

- **HTML y C# limpios:** No requiere componentes personalizados ni mezcla lógica .Net con HTML. El frontend sigue siendo HTML5 y el backend, .Net puro.

- **Desacople real:** Mantiene la separación clara entre lógica de negocio (backend) y presentación (frontend), facilitando el trabajo colaborativo y el mantenimiento.

- **Enfoque declarativo:** El mapeo entre ViewModels y elementos HTML se define de forma simple y expresiva.

- **Fácil integración:** Solo se necesita definir los mapeos y llamar a un método de actualización. El resto es transparente para el desarrollador.

- **Manipulación del DOM eficiente:** Manipula los objetos del DOM en memoria todo lo posible y sólo actualiza la UI cuando es estrictamente necesario, optimizando la performance.

- **Compatible y escalable:** Compatible desde HTML vainilla, Web Components, a cualquier típica herramienta de frontend que produzca HTML.

- **Modularización del HTML:** Permite modularizar la estructura del documento en fragmentos de HTML contenidos en templates o URIs que el propio usuario define y maqueta a gusto.

---

## Funcionalidades

- **Seguimiento de cambios:** Soporta el seguimiento de cambios para transmitir y actualizar en la UI solo los datos que realmente cambiaron desde la última actualización. Esto optimiza el tráfico de datos y evita renders innecesarios.

- **Estrategias flexibles de selección de elementos:** Contiene una serie de estrategias de selección de elementos HTML, adaptándose a cualquier estructura de vista.

- **Mapeo a funciones javascript personalizadas:** Permite invocar funciones de javascript personalizadas que se ejecutan ante los cambios en el viewmodel.

- **Modularizacion del mapeo:** El mapeo puede subdividirse en varios submapeos para una mayor legibilidad o reutilización.

- **Cache de funciones autogeneradas:** Las funciones JavaScript de inicialización y actualización de la vista, generadas automáticamente por Vitraux, pueden cachearse optimizando la carga.

- **Utilización de Shadow DOM:** Puede utilizar Shadow DOM para encapsular estilos y estructura según la necesidad del proyecto.

---

## Primeros pasos

1. **Crea un proyecto Blazor WebAssembly Standalone**  
   Usa Visual Studio asegurándote de desactivar la opción “Incluir páginas de ejemplo” o la CLI de .NET con el comando
   ```cli
   dotnet new blazorwasm --empty -o MyProject
1. **Elimina todos los archivos `.razor`**  
   Borra todos los archivos con extensión `.razor` de la solución para dejar el proyecto limpio, sin páginas ni componentes de Blazor.

   ![Remove .razor Files](https://github.com/sebastiantramontana/Vitraux/blob/main/assets/readme/remove-razor-files.png)

1. **Elimina el código de root components del método `Main()`**  
   Quita el registro y renderizado de componentes raíz en el método `Main()`, ya que Vitraux no los utiliza.

   ![Remove Root Components](https://github.com/sebastiantramontana/Vitraux/blob/main/assets/readme/remove-root-components-main.png)

1. **[Instala](#instalación) el paquete de Vitraux desde Nuget**

1. **Agrega el archivo JavaScript de Vitraux**  
   Coloca el archivo `vitraux-<version>-min.js` en la carpeta que prefieras de tu sitio web. No hay una ubicación obligatoria; solo asegúrate de referenciar la ruta correcta en el HTML.

1. **Incluye las referencias a los archivos JavaScript en tu HTML**  
   Agrega la referencia a `vitraux-<version>-min.js` en el `<head>` o antes del cierre de `</body>`.  
   La referencia a `_framework/blazor.webassembly.js` debe ir al final del `<body>`:

   ```html
   <script src="js/vitraux-<version>-min.js"></script>
   ...
   <script src="_framework/blazor.webassembly.js" autostart="false"></script>

1. **Agrega tu código C# y configuración mínima de ejemplo:**
   ```csharp
   // PetOwner.cs
   public record class PetOwner
   {
      public int Id { get; init; }
      public string Name { get; init; } = string.Empty;
      public string Address { get; init; } = string.Empty;
      public string? PhoneNumber { get; init; }
      public string HtmlComments { get; init; } = string.Empty;
      public IEnumerable<Pet> Pets { get; init; } = Enumerable.Empty<Pet>();
   }

   // Pet.cs
   public record class Pet
   {
      public string Name { get; init; } = string.Empty;
      public DateTime DateOfBirth { get; init; }
   }

   // PetOwnerConfiguration.cs
   public class PetOwnerConfiguration : IModelConfiguration<PetOwner>
   {
      public ConfigurationBehavior ConfigurationBehavior { get; } = new()
      {
         QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
         TrackChanges = true,
         VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("test 1.0")
      };

      public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> modelMapper)
         => modelMapper
               .MapValue(po => po.Name).ToElements.ById("petowner-name").ToContent
               .MapValue(po => po.Address).ToElements.ById("petowner-address").ToContent
               .MapValue(po => po.PhoneNumber).ToElements.ById("petowner-phone-number").ToContent
               .MapValue(po => po.HtmlComments).ToElements.ByQuery(".comments").ToHtml
               .MapCollection(po => po.Pets)
                  .ToTables.ById("petowner-pets")
                  .PopulatingRows.ToTBody(0).FromTemplate("petowner-pet-row")
                     .MapValue(pet => pet.Name).ToElements.ByQuery("[data-id='pet-name']").ToContent
                     .MapValue(pet => pet.DateOfBirth).ToElements.ByQuery("[data-id='pet-date-of-birth']").ToContent
                  .EndCollection
               .EndCollection
               .Data;
   }

   // Program.cs
   using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
   using Vitraux;
   using System.Runtime.InteropServices.JavaScript;

   public partial class Program
   {
      private static IViewUpdater<PetOwner> _petownerViewUpdater = default!;

      public static async Task Main(string[] args)
      {
         var builder = WebAssemblyHostBuilder.CreateDefault(args);

         _ = builder.Services
               .AddVitraux() //add Vitraux
               .AddConfiguration(() => new VitrauxConfiguration { UseShadowDom = true }) //add a configuration (optional)
               .AddModelConfiguration<PetOwner, PetOwnerConfiguration>(); //add the model configuration

         await using var host = builder.Build();

         await host.Services.BuildVitraux(); //Build Vitraux

         _petownerViewUpdater = host.Services.GetRequiredService<IViewUpdater<PetOwner>>(); //Get your view updater

         await host.RunAsync();
      }

      [JSExport]
      public static async Task GetPetOwner()
      {
         var petOwner = new PetOwner
         {
               Id = 1,
               Name = "Juan Pérez",
               Address = "Av. Siempre Viva 742",
               PhoneNumber = "+54 9 11 1234-5678",
               HtmlComments = "<b>Cliente VIP</b>",
               Pets = new[]
               {
                  new Pet { Name = "Boby", DateOfBirth = new DateTime(2021, 5, 1) },
                  new Pet { Name = "Miau", DateOfBirth = new DateTime(2019, 9, 14) }
               }
         };

         await _petownerViewUpdater.Update(petOwner); //Update the UI
      }
   }

1. **Ejemplo de HTML completo:**
   ```html
   <!DOCTYPE html>
   <html lang="en">
   <head>
      <meta charset="utf-8" />
      <title>Vitraux Example</title>
      <link rel="stylesheet" href="styles.css" />
      <script src="js/vitraux.js"></script>
      <script>
         addEventListener("DOMContentLoaded", async () => {
               await Blazor.start();
               const petOwnerWasm = await getPetOwnerWasm();
               await petOwnerWasm.GetPetOwner();

               async function getPetOwnerWasm() {
                  const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
                  const exports = await getAssemblyExports("YourProject.dll");
                  return exports.YourNamespace.Program;
               }
         });
      </script>
   </head>
   <body>
      <h1>Petowner Example</h1>
      <div class="info-section">
         <div>Owner Information</div>
         <div id="petowner-name"></div>
         <div id="petowner-address"></div>
         <div id="petowner-phone-number"></div>
      </div>
      <div class="info-section">
         <div>Pets</div>
         <table id="petowner-pets">
               <thead>
                  <tr>
                     <th>Name</th>
                     <th>Date of Birth</th>
                  </tr>
               </thead>
               <tbody></tbody>
         </table>
      </div>
      <div class="info-section">
         <div>Comments</div>
         <div class="comments"></div>
      </div>

      <template id="petowner-pet-row">
         <tr>
               <td data-id="pet-name"></td>
               <td data-id="pet-date-of-birth"></td>
         </tr>
      </template>

      <script src="_framework/blazor.webassembly.js" autostart="false"></script>
   </body>
   </html>

1. **Para una detalle completo de las funcionalidades y detalles técnicos consulta el [Manual de Referencia](./docs/reference-manual.md).**

---

## Cómo Funciona
Vitraux funciona mediante el mapeo declarativo de viewmodels .NET a elementos HTML estándar en aplicaciones WebAssembly.
Cuando necesitas actualizar la interfaz, simplemente se llama a una función de actualización en .NET que envía los datos al navegador. Allí, una función JavaScript generada por Vitraux actualiza el DOM automáticamente, manteniendo la vista sincronizada con el modelo de datos.

## Instalación
Instala el paquete desde [NuGet](https://www.nuget.org/packages/Vitraux/)

## Contribuciones
¡Las contribuciones son bienvenidas! Si tienes ideas, encuentras errores o deseas colaborar, no dudes en abrir un Issue o un Pull Request.

## Licencia
Este proyecto está licenciado bajo los términos de la licencia MIT.
Consulta el archivo [LICENSE](https://github.com/sebastiantramontana/Vitraux/blob/main/LICENSE) para más información.