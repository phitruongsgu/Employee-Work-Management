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
    public class Account_RoleRepository : IAccount_RoleRepository
    {
        private readonly EFContext context;
        public Account_RoleRepository(EFContext context)
        {
            this.context = context;
        }
        public void createAccount_Role(Account_Role Account_Role)
        {
            context.Account_Roles.Add(Account_Role);
            context.SaveChanges();
        }

        public void editAccount_Role(Account_Role Account_Role)
        {
            context.Account_Roles.Update(Account_Role);
            context.SaveChanges();
        }

        public Account_Role FindByID(int id)
        {
            return context.Account_Roles.Find(id);
        }


        public void removeAccount_Role(int id)
        {
            context.Account_Roles.Remove(context.Account_Roles.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Account_Role> Account_Roles()
        {
            return context.Account_Roles.ToList();
        }

       
    }
}
