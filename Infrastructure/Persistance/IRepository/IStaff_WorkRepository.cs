using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IStaff_WorkRepository
    {
        Staff_Work FindByID(int id);
        IEnumerable<Staff_Work> Staff_Works();
        void createStaff_Work(Staff_Work Staff_Work);
        void editStaff_Work(Staff_Work Staff_Work);
        void removeStaff_Work_By_WorkID(int id);
        void removeStaff_Work_By_StaffID(int id);
        void removeStaff_Work(int idStaff, int idWork);
    }
}
