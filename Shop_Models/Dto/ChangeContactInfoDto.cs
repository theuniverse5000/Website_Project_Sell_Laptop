using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ChangeContactInfoDto
    {
        public string FullName { get; set; }
        public string NewAddress { get; set; }
        public string NewPhoneNumber { get; set; }
    }
}
