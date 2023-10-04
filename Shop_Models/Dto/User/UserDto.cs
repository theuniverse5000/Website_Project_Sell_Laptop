using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Models.Entities;


namespace Shop_Models.Dto.User
{
    public class UserDto : Shop_Models.Entities.User
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string? roleNames { get; set; }
    }
}
