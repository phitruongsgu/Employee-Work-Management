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
    public class WorkRepository: IWorkRepository
    {
        private readonly EFContext context;
        public WorkRepository(EFContext context)
        {
            this.context = context;
        }
        public void createWork(Work Work)
        {
            context.Works.Add(Work);
            context.SaveChanges();
        }

        public void editWork(Work Work)
        {
            context.Works.Update(Work);
            context.SaveChanges();
        }

        public Work FindByID(int id)
        {
            return context.Works.Find(id);
        }


        public void removeWork(int id)
        {
            context.Works.Remove(context.Works.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Work> Works()
        {
            return context.Works.ToList();
        }
    }
       
}
