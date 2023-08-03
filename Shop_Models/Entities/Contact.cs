using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Sai định dạng email!")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

        public string CodeManagePost { get; set; }

        public string Website { get; set; }
        public bool Status { get; set; }
    }
}
