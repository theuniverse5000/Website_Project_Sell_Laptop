using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Models.Entities
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        public Guid Id { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public Guid BillId { get; set; }
        public string? SerialNumber { get; set; }
        public virtual Bill? Bill { get; set; }
    }
}
