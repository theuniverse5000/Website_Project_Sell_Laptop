using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto.Account
{
    public class LoginResponesDto
    {
        public string Mess { get; set; }
        public bool Successful { get; set; }
        public object Data { get; set; }
    }
}
