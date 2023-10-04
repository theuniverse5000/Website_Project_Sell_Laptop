using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("Role")]
    public class Role: IdentityRole<Guid>
    {
        public int Status { get; set; }
    }
}
