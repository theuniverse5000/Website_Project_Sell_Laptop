using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key] public Guid Id { get; set; }
        [MaxLength(30)]
        public string? Username { get; set; }
        [MaxLength(30)]
        public string? Password { get; set; }
        [MaxLength(150)]
        public string? FullName { get; set; }
        [MaxLength(20)]
        public int? PhoneNumber { get; set; }
        [MaxLength(150)]
        public string? Address { get; set; }
        public int Status { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ViDiem? ViDiem { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }

    }
}
