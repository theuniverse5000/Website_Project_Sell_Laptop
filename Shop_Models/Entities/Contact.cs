using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Sai định dạng email!")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public DateTime CreateDate { get; set; }

        public string? CodeManagePost { get; set; }

        public string? Website { get; set; }
        public int Status { get; set; }
    }
}
