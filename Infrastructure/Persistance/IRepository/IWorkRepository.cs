using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IWorkRepository
    {
        Work FindByID(int id);
        IEnumerable<Work> Works();
        void createWork(Work Work);
        void editWork(Work Work);
        void removeWork(int id);
    }
}
