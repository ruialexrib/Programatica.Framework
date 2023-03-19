namespace Programatica.Framework.Data.Models
{
    /// <summary>
    /// Represents a change made to a property of an entity.
    /// </summary>
    public class TrackChange : BaseModel, IModel
    {
        /// <summary>
        /// Gets or sets the audit associated with this change.
        /// </summary>
        public virtual Audit Audit { get; set; }

        /// <summary>
        /// Gets or sets the ID of the audit associated with this change.
        /// </summary>
        public int AuditId { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that was changed.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the old value of the property before the change was made.
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value of the property after the change was made.
        /// </summary>
        public string NewValue { get; set; }

    }
}
