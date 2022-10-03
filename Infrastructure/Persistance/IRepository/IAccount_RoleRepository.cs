using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.IRepository
{
    public interface IAccount_RoleRepository
    {
        Account_Role FindByID(int id);
        IEnumerable<Account_Role> Account_Roles();
        void createAccount_Role(Account_Role Account_Role);
        void editAccount_Role(Account_Role Account_Role);
        void removeAccount_Role(int id);
    }
}
