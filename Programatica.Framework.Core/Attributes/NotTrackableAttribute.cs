using System;

namespace Programatica.Framework.Core.Attributes
{
    /// <summary>
    /// Indicates that the property or class marked with this attribute should not be tracked by an entity framework context.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class NotTrackableAttribute : Attribute
    {
    }
}
