using Programatica.Framework.Core.Attributes;
using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Programatica.Framework.Data.Extensions
{
    public static class ModelExtensions
    {
        public static List<Variance> TrackChanges<IModel>(this IModel val1, IModel val2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] props = val1.GetType().GetProperties();
            var notMappedProps = props.Where(pi => pi.GetCustomAttributes(typeof(NotTrackableAttribute), false).Length > 0);
            foreach (PropertyInfo p in props.Except(notMappedProps))
            {
                if (!typeof(IModel).IsAssignableFrom(p.PropertyType) &&
                    !typeof(List<IModel>).IsAssignableFrom(p.PropertyType))
                {
                    Variance v = new Variance();
                    v.Prop = p.Name;
                    v.valA = p.GetValue(val1);
                    v.valB = p.GetValue(val2);

                    if (v.valA == null) v.valA = "";
                    if (v.valB == null) v.valB = "";

                    if (!v.valA.ToString().Equals(v.valB.ToString()))
                        variances.Add(v);
                }
            }
            return variances;
        }
        public static Type GetProxyType(this IModel model)
        {
            var thisType = model.GetType();

            if (thisType.Namespace == "System.Data.Entity.DynamicProxies")
                return thisType.BaseType;

            return thisType;
        }
    }

    public class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }
}
