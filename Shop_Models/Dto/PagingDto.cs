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
        public string? Ten { get; set; }
        public string? ThongSo { get; set; }
        public string? ChatLieu { get; set; }
        public string? KichCo { get; set; }
        public string? TanSo { get; set; }
        public string? LinkImage { get; set; }
        public int Status { get; set; }
    }
}
