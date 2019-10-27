namespace Blacksmith.PagedEnumerable.Tests.Models
{
    public enum UserDetailsColumns
    {
        UserName,
        RoleName,
        Active,
    }

    public class UserDetails
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
    }
}