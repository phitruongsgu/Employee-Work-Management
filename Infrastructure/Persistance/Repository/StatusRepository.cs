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
    public class StatusRepository : IStatusRepository
    {
        private readonly EFContext context;
        public StatusRepository(EFContext context)
        {
            this.context = context;
        }
        public void createStatus(Status Status)
        {
            context.Statuses.Add(Status);
            context.SaveChanges();
        }

        public void editStatus(Status Status)
        {
            context.Statuses.Update(Status);
            context.SaveChanges();
        }

        public Status FindByID(int id)
        {
            return context.Statuses.Find(id);
        }


        public void removeStatus(int id)
        {
            context.Statuses.Remove(context.Statuses.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Status> Statuses()
        {
            return context.Statuses.ToList();
        }
    }
}
