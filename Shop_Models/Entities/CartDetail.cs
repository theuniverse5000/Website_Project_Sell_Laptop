using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("CartDetail")]
    public class CartDetail
    {
        [Key] public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IDProductDetails { get; set; }
        public int Quantity { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }


    }
}
