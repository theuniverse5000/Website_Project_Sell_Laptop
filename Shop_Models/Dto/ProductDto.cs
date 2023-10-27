using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }
        public int Status { get; set; }
        public string? ManuName { get; set; }
        public string? ProductTypeName { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
