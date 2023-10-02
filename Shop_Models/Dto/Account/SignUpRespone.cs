using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto.Account
{
    public class SignUpRespone
    {
        public string Mess { get; set; }
        public List<IdentityError>? Data { get; set; }
    }
}
