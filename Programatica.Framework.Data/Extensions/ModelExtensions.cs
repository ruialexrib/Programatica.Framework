using Programatica.Framework.Core.Attributes;
using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Programatica.Framework.Data.Extensions
{
    /// <summary>
    /// A static class containing extension methods for tracking changes and getting proxy types of models.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// Returns a list of variances between two instances of a model that implement the IModel interface.
        /// The method excludes properties marked with the NotTrackableAttribute.
        /// </summary>
        /// <typeparam name="IModel">The interface implemented by the models being compared.</typeparam>
        /// <param name="val1">The first instance of the model.</param>
        /// <param name="val2">The second instance of the model.</param>
        /// <returns>A List of Variance objects.</returns>
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

                    if (v.valA == null) v.valA = string.Empty;
                    if (v.valB == null) v.valB = string.Empty;

                    if (!v.valA.ToString().Equals(v.valB.ToString()))
                        variances.Add(v);
                }
            }
            return variances;
        }

        /// <summary>
        /// Returns the base type of the given model if it is a dynamic proxy type, otherwise returns the model's type.
        /// </summary>
        /// <param name="model">An instance of a model.</param>
        /// <returns>The base type of the model if it is a dynamic proxy type, otherwise the model's type.</returns>
        public static Type GetProxyType(this IModel model)
        {
            var thisType = model.GetType();

            if (thisType.Namespace == "System.Data.Entity.DynamicProxies")
                return thisType.BaseType;

            return thisType;
        }
    }

    /// <summary>
    /// Represents a variance between two property values in a model.
    /// </summary>
    public class Variance
    {
        /// <summary>
        /// The name of the property that has a variance between two models.
        /// </summary>
        public string Prop { get; set; }

        /// <summary>
        /// The value of the property in the first instance of the model being compared.
        /// </summary>
        public object valA { get; set; }

        /// <summary>
        /// The value of the property in the second instance of the model being compared.
        /// </summary>
        public object valB { get; set; }
    }
}
