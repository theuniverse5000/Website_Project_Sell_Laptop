using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    public class Token
    {
        public string RefreshToken { get; set; }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsActive { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime Expired { get; set; } // het han khi nao
        public DateTime Iaced { get; set; }
    }
}
