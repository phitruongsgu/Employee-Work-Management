using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyCongViecNhanVien.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bắt buộc nhập tên tài khoản")]
        [Display(Name = "Tài khoản (Email)")]
        public String username { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} kí tự và tối đa là {1} kí tự.", MinimumLength = 6)]
        [Compare("password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public String password { get; set; }
    }
}
