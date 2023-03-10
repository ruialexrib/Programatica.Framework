using System;

namespace Programatica.Framework.Data.Models
{
    /// <summary>
    /// Represents the base interface for all models.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets or sets the system ID of the model.
        /// </summary>
        Guid SystemId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the model.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the comments of the model.
        /// </summary>
        string Comments { get; set; }

        /// <summary>
        /// Gets or sets the created date of the model.
        /// </summary>
        DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the model.
        /// </summary>
        string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the last modified date of the model.
        /// </summary>
        DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the model.
        /// </summary>
        string LastModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the last destroyed date of the model.
        /// </summary>
        DateTime? LastDestroyedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last destroyed the model.
        /// </summary>
        string LastDestroyedUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the model is a system model.
        /// </summary>
        bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the model is destroyed.
        /// </summary>
        bool IsDestroyed { get; set; }
    }
}