using Core.Entities;
using Infrastructure.Persistance.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IStaffRepository
    {
        Staff FindByID(int id);
        IEnumerable<Staff> Staffs();
        void createStaff(Staff Staff);
        void editStaff(Staff Staff);
        void removeStaff(int id);
    }
}
