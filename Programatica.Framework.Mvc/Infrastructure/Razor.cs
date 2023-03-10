using shortid;
using shortid.Configuration;

namespace Programatica.Framework.Mvc.Infrastructure
{
    public struct Razor
    {
        public static string Rnd()
        {
            return ShortId.Generate(new GenerationOptions(useNumbers: false,
                                                          useSpecialCharacters: false,
                                                          length: 8));
        }

        public static string GenerateElementId(string prefix)
        {
            var rnd = Rnd();
            return $"{prefix}_id_{rnd}";
        }
    }
}
