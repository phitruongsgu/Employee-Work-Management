using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.ViewModel
{
    public class HomeViewModel
    {
        public int countWorks { get; set; }
        public int countDoingForWorks { get; set; }
        public int countLateForWorks { get; set; }
        public int countFinishForWorks { get; set; }
        public IEnumerable<Status> GetStatuses { get; set; }
        public IEnumerable<Work> GetWorks { get; set; }
    }
}
