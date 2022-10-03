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
    public class AnnounceRepository : IAnnounceRepository
    {
        private readonly EFContext context;
        public AnnounceRepository(EFContext context)
        {
            this.context = context;
        }
        public void createAnnounce(Announce Announce)
        {
            context.Announces.Add(Announce);
            context.SaveChanges();
        }

        public void editAnnounce(Announce Announce)
        {
            context.Announces.Update(Announce);
            context.SaveChanges();
        }

        public Announce FindByID(int id)
        {
            return context.Announces.Find(id);
        }


        public void removeAnnounce(int id)
        {
            context.Announces.Remove(context.Announces.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Announce> Announces()
        {
            return context.Announces.ToList();
        }

       
    }
}
