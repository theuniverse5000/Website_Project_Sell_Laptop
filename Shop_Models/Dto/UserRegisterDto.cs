using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Phải Nhập {0}")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phải Nhập {0}")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1}", MinimumLength = 3)]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phải Nhập {0}")]
        [StringLength(100,ErrorMessage ="{0} phải dài từ {2} đến {1}",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Lặp lại mật khẩu")]
        [Compare("Password",ErrorMessage = "Mật khẩu lặp lại không chính xác")]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsAdmin { get; set; }
    }
}
