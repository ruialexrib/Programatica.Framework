namespace Programatica.Framework.Mvc.Options
{
    public class ClaimBasedAuthAdapterOptions
    {
        public string UserNameFieldName{ get; set; }
        public string PasswordFieldName { get; set; }
        public string LastLoginDateTimeFieldName { get; set; }
        public string UserRoleFieldName { get; set; }
        public string UserRolePermissionFieldName { get; set; }
    }
}
