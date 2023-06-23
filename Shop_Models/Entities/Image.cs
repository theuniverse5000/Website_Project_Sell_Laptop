using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key] public Guid Id { get; set; }
        public string Ma { get; set; }
        public string LinkImage { get; set; }
        public int Status { get; set; }
        public Guid IdProductDetail { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
