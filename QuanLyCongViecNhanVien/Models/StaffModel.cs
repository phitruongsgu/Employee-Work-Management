using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.Models
{
    public class StaffModel
    {
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên nhân viên!!!")]
        [Display(Name = "Tên nhân viên")]
        public string Staff_Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh của nhân viên!!!")]
        [Display(Name = "Ngày sinh")]   
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số chứng minh nhân dân!!!")]
        [Display(Name = "Chứng minh nhân dân")]
        public string ID_Card { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính!!!")]
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Địa chỉ Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái!!!")]
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public int Position_ID { get; set; }
        public int Account_ID { get; set; }

    }
}
