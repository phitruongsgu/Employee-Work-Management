using Core.Entities;
using QuanLyCongViecNhanVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCongViecNhanVien.ViewModel
{
    public class ModelView
    {
        public IEnumerable<Account> Accounts { get; set; }
        public Account account { get; set; }

        public IEnumerable<Announce> Announces { get; set; }
        public Announce announce { get; set; }

        public IEnumerable<Staff> Staffs { get; set; }
        public Staff staff { get; set; }

        public IEnumerable<Role> Roles { get; set; }
        public Role role { get; set; }

        public IEnumerable<Position> Positions { get; set; }
        public Position position { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public Comment comment { get; set; }

        public IEnumerable<Status> Statuses { get; set; }
        public Status status { get; set; }

        public IEnumerable<Work> Works { get; set; }
        public Work work { get; set; }

        public IEnumerable<Staff_Work> Staff_Works { get; set; }
        public IEnumerable<Account_Role> Account_Roles { get; set; }
        public int Staff_ID { get; set; }
        public int Account_ID { get; set; }

        public List<Staff> getStaffNotDoWork { get; set; }

        public WorkModel WM { get; set; }
        public StaffModel SM { get; set; }
    }
}
