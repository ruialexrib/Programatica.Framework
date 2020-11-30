using System.Collections.Generic;
using System.Reflection;

namespace Programatica.Framework.Mvc.Models
{
    public class MvcActionInfo
    {
        public string Id => $"/{ControllerId}/{Name}";

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ControllerId { get; set; }
        public IEnumerable<CustomAttributeData> CustomAttributes { get; set; }
    }
}
