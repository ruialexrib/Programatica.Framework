using System;

namespace Programatica.Framework.Data.Models
{
    public class Audit : BaseModel, IModel
    {
        public Guid ContentSystemId { get; set; }
        public int ContentId { get; set; }
        public string ContentType { get; set; }
        public string ContentFunction { get; set; }
    }
}
