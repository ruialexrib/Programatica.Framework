using Programatica.Framework.Core;
using System;

namespace Programatica.Framework.Data.Models
{
    public interface IModel 
    {
        Guid SystemId { get; set; }
        int Id { get; set; }
        string Comments { get; set; }
        DateTime? CreatedDate { get; set; }
        string CreatedUser { get; set; }
        DateTime? LastModifiedDate { get; set; }
        string LastModifiedUser { get; set; }
        DateTime? LastDestroyedDate { get; set; }
        string LastDestroyedUser { get; set; }
        bool IsSystem { get; set; }
        bool IsDestroyed { get; set; }
    }
}
