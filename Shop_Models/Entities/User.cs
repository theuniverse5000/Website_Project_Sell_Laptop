using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("User")]
    public class User : IdentityUser<Guid>
    {
        public string? Password { get; set; }
        public int Status { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Guid?RoleId { get; set; }
    }
}
