using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Cart")]
    public class Cart
    {
        [Key] public Guid IdUser { get; set; }
        public string Description { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartDetail>? CartDetails { get; set; }
    }
}
