using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("ManagePost")]
    public class ManagePost
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }

        public string LinkImage { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
