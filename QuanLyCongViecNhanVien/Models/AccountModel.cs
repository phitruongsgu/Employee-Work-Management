using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.Models
{
    public class AccountModel
    {
        public int Account_ID { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập tên tài khoản!!!")]
        [Display(Name = "Tên tài khoản")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!!!")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} kí tự và tối đa là {1} kí tự.", MinimumLength = 6)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email!!!")]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [Display(Name = "Trạng thái tài khoản")]
        public bool Account_Status { get; set; }

        public int Role_ID { get; set; }

    }
}
