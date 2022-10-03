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
    public class Staff_WorkRepository : IStaff_WorkRepository
    {
        private readonly EFContext context;
        public Staff_WorkRepository(EFContext context)
        {
            this.context = context;
        }
        public void createStaff_Work(Staff_Work Staff_Work)
        {
            context.Staff_Works.Add(Staff_Work);
            context.SaveChanges();
        }

        public void editStaff_Work(Staff_Work Staff_Work)
        {
            context.Staff_Works.Update(Staff_Work);
            context.SaveChanges();
        }

        public Staff_Work FindByID(int id)
        {
            return context.Staff_Works.Find(id);
        }


        public void removeStaff_Work_By_WorkID(int id)
        {
            List<Staff_Work> list = context.Staff_Works.Where(p => p.Work_ID == id).ToList();
            foreach(var s in list)
            {
                context.Staff_Works.Remove(s);
            }
            context.SaveChanges();
        }

        public void removeStaff_Work_By_StaffID(int id)
        {
            List<Staff_Work> list = context.Staff_Works.Where(p => p.Staff_ID == id).ToList();
            foreach (var s in list)
            {
                context.Staff_Works.Remove(s);
            }
            context.SaveChanges();
        }
        
        public void removeStaff_Work(int idStaff, int idWork)
        {
            Staff_Work staff_Work = Staff_Works().Where(p => p.Staff_ID == idStaff && p.Work_ID == idWork).FirstOrDefault();
            context.Remove(staff_Work);
            context.SaveChanges();
        }

        public IEnumerable<Staff_Work> Staff_Works()
        {
            return context.Staff_Works.ToList();
        }
    }
}
