using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key] public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public Guid IdRole { get; set; }
        public virtual Role? Role { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ViDiem? ViDiem { get; set; }
        public virtual ICollection<Bill>? Bills { get; set;}
       
    }
}
