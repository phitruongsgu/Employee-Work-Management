using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.Models
{
    public class WorkModel
    {
        public int WorkID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề cho công việc!!!")]
        [Display(Name = "Tiêu đề")]
        public String Title { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu công việc!!!")]
        [Display(Name = "Ngày bắt đầu")]
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc công việc!!!")]
        [Display(Name = "Ngày kết thúc")]
        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Phạm vi")]
        public string Range { get; set; }

        [Display(Name = "Tên file")]
        public string FileName { get; set; }

        [Display(Name = "Trạng thái công việc")]
        public int StatusID { get; set; }
    }

    public class CommentAjax
    {
        public String content { get; set; }
        public String workID { get; set; }
    }
}
