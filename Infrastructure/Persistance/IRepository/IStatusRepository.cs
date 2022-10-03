using Core.Entities;
using Infrastructure.Persistance.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IStatusRepository
    {
        Status FindByID(int id);
        IEnumerable<Status> Statuses();
        void createStatus(Status status);
        void editStatus(Status status);
        void removeStatus(int id);
    }
}
