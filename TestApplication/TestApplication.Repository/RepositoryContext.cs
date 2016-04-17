using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TestApplication.Repository.dbo;

namespace TestApplication.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<CustomerDbo> Customers { get; set; }

        public DbSet<OrderDbo> Orders { get; set; }

        static RepositoryContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RepositoryContext, Migrations.Configuration>());
        }
    }
}
