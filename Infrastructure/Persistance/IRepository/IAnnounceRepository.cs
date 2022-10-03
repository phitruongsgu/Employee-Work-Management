using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IAnnounceRepository
    {
        Announce FindByID(int id);
        IEnumerable<Announce> Announces();
        void createAnnounce(Announce Announce);
        void editAnnounce(Announce Announce);
        void removeAnnounce(int id);
    }
}
