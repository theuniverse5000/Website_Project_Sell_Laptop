using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Models;


namespace Shop_Models.Dto.User
{
    public class UserDto : Shop_Models.Entities.User
    {
        public string? roleNames { get; set; }
    }
}
