using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]   public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Descripion { get; set; }
        public int Status { get; set; }
        public virtual ICollection<User>? User { get; set; }
    }
}
