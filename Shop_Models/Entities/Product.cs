using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key] public Guid ID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public Guid? IdManufacturer { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
