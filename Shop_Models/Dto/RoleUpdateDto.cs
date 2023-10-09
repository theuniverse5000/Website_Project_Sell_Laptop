using System.ComponentModel.DataAnnotations;

namespace Shop_Models.Dto
{
    public class RoleUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? normalizedName { get; set; }

        public string? concurrencyStamp { get; set; }
        public int status { get; set; }
    }
}