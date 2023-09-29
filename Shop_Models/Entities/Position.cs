using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    public class Position : IdentityRole<Guid>
    {
        public int status { get; set; }
    }
}
