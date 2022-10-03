using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DBcontext
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options)
           : base(options)
        {

        }
      
      
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account_Role>().HasKey(p =>new
            {
                p.Account_ID,
                p.Role_ID
            });
            modelBuilder.Entity<Staff_Work>().HasKey(p => new
            {
                p.Staff_ID,
                p.Work_ID
            });
        }
        public DbSet<Account_Role> Account_Roles { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Announce> Announces { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Work> Works { get; set; }

        
        public DbSet<Staff_Work> Staff_Works { get; set; }

    }
}
