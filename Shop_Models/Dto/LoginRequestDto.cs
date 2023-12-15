using System.ComponentModel.DataAnnotations;

namespace Shop_Models.Dto
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? FullName { get; set; }
        public bool? isAdmin { get; set; }
    }
}