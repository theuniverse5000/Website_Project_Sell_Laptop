using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class PagingDto
    {
        public Guid Id { get; set; }
        public string? Ma { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
    }
}
