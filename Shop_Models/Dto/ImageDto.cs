using Shop_Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(150)]
        public string? LinkImage { get; set; }
        public int Status { get; set; }
        public Guid ProductDetailId { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }

    }
}
