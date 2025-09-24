using Vitraux.Helpers;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal class JsObjectNamesGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : IJsObjectNamesGenerator
{
    public string GenerateUniqueObjectName(string namePrefix, string initialName)
    {
        var numberedNamePosfix = atomicAutoNumberGenerator.Next();

        return string.IsNullOrWhiteSpace(namePrefix)
                ? $"{initialName}{numberedNamePosfix}"
                : $"{namePrefix}_{initialName}{numberedNamePosfix}";
    }
}
