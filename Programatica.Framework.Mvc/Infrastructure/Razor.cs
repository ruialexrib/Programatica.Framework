using shortid;
using shortid.Configuration;

namespace Programatica.Framework.Mvc.Infrastructure
{
    public struct Razor
    {
        public static string Rnd()
        {
            return ShortId.Generate(new GenerationOptions
            {
                UseNumbers = false,
                UseSpecialCharacters = false,
                Length = 8
            });
        }
    }
}
