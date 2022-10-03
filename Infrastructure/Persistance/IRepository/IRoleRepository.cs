using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IRoleRepository
    {
        Role FindByID(int id);
        IEnumerable<Role> Roles();
        void createRole(Role Role);
        void editRole(Role Role);
        void removeRole(int id);
    }
}
