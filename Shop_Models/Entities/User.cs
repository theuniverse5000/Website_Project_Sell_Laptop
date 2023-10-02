using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("User")]
    public class User : IdentityUser<Guid>
    {
        [Key] public Guid Id { get; set; }
        public string? UserName { get; set; } = "";
        public string? Password { get; set; } = "";
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ViDiem? ViDiem { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
