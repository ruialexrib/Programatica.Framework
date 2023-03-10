using shortid;
using shortid.Configuration;

namespace Programatica.Framework.Mvc.Infrastructure
{
    public struct Razor
    {
        public static string Rnd()
        {
            var options = new GenerationOptions(useNumbers: false, useSpecialCharacters: false, length: 8);
            return ShortId.Generate(options);
        }

        public static string GenerateElementId(string prefix)
        {
            var rnd = Rnd();
            return $"{prefix}_id_{rnd}";
        }
    }
}
