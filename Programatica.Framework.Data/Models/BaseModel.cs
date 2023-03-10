using Programatica.Framework.Core.Attributes;
using System;

namespace Programatica.Framework.Data.Models
{
    /// <summary>
    /// This class represents the base model for all entities in the system.
    /// It implements the IModel interface and provides common properties that are shared by all entities.
    /// All properties mentioned above are decorated with the <see cref="NotTrackableAttribute" /> attribute, which means they 
    /// should not be included in the changes tracked by the DbContext's ChangeTracker
    /// </summary>
    public abstract class BaseModel : IModel
    {
        /// <summary>
        /// Gets or sets the system identifier for the entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public Guid SystemId { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the comments for the entity.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was created.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was last modified.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public string LastModifiedUser { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was last destroyed.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public DateTime? LastDestroyedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last destroyed the entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public string LastDestroyedUser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is a system entity.
        /// </summary>
        /// <remarks>
        /// This property is decorated with the <see cref="NotTrackableAttribute"/>,
        /// which means it should not be included in changes tracked by the ChangeTracker
        /// of the DbContext.
        /// </remarks>
        [NotTrackable]
        public bool IsSystem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is destroyed.
        /// </summary>
        public bool IsDestroyed { get; set; }

        /// <summary>
        /// Initializes a new instance of the BaseModel class.
        /// </summary>
        public BaseModel()
        {
            SystemId = Guid.NewGuid();
        }
    }
}