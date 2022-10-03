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
    public class RoleRepository : IRoleRepository
    {
        private readonly EFContext context;
        public RoleRepository(EFContext context)
        {
            this.context = context;
        }
        public void createRole(Role Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
        }

        public void editRole(Role Role)
        {
            context.Roles.Update(Role);
            context.SaveChanges();
        }

        public Role FindByID(int id)
        {
            return context.Roles.Find(id);
        }


        public void removeRole(int id)
        {
            context.Roles.Remove(context.Roles.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Role> Roles()
        {
            return context.Roles.ToList();
        }
    }
}
