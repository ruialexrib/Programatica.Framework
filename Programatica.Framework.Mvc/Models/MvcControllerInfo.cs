using System.Collections.Generic;
using System.Reflection;

namespace Programatica.Framework.Mvc.Models
{
    public class MvcControllerInfo
    {
        public string Id => Name;

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<CustomAttributeData> CustomAttributes { get; set; }

        public IEnumerable<MvcActionInfo> Actions { get; set; }
    }
}
