using System;

namespace Programatica.Framework.Data.Models
{
    /// <summary>
    /// Represents an audit log entry for a specific content item.
    /// </summary>
    public class Audit : BaseModel, IModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the content system associated with this audit entry.
        /// </summary>
        public Guid ContentSystemId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the content item associated with this audit entry.
        /// </summary>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the type of content associated with this audit entry.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the function associated with the content item (e.g. created, updated, deleted).
        /// </summary>
        public string ContentFunction { get; set; }
    }
}
