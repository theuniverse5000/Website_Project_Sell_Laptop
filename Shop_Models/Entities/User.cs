using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("User")]
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public Guid? RoleId { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ViDiem? ViDiem { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }

    }
}
