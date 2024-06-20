using ChocoLink.Data.EntityFramework.Configuration;
using ChocoLink.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Data.EntityFramework
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Invite> Invites { get; set; }

        public Context() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseSqlServer(@"Data source = 201.62.57.93,1434; 
                                    Database = BD045201; 
                                    User ID = RA045201; 
                                    Password = 045201;
                                    TrustServerCertificate = True;");
#else
            optionsBuilder.UseSqlServer(@"Data source = 10.107.176.41, 1434; 
                                    Database = BD045201; 
                                    User ID = RA045201; 
                                    Password = 045201;
                                    TrustServerCertificate = True;");
#endif

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupUserConfiguration());
            modelBuilder.ApplyConfiguration(new InviteConfiguration());
        }
    }
}
