using Core.Entities;
using Infrastructure.Persistance.DBcontext;
using Infrastructure.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly EFContext context;
        public StaffRepository(EFContext context)
        {
            this.context = context;
        }
        public void createStaff(Staff Staff)
        {
            context.Staffs.Add(Staff);
            context.SaveChanges();
        }

        public void editStaff(Staff Staff)
        {
            context.Staffs.Update(Staff);
            context.SaveChanges();
        }

        public Staff FindByID(int id)
        {
            return context.Staffs.Find(id);
        }


        public void removeStaff(int id)
        {
            context.Staffs.Remove(context.Staffs.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Staff> Staffs()
        {
            return context.Staffs.ToList();
        }
    }
}
