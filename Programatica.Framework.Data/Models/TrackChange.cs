namespace Programatica.Framework.Data.Models
{
    public class TrackChange : BaseModel, IModel
    {
        public virtual Audit Audit { get; set; }
        public int AuditId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
