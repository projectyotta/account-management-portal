using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMC.Models;

namespace CMC.DAL
{
    public class CMCContext : DbContext
    {


        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
 


        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Contract>()
             .HasMany(c => c.Employees).WithMany(i => i.Contracts)
             .Map(t => t.MapLeftKey("ContractID")
                 .MapRightKey("EmployeeID")
                 .ToTable("ContractEmployee"));
            modelBuilder.Entity<Job>().MapToStoredProcedures();


        }

        public System.Data.Entity.DbSet<CMC.Models.Photo> Photos { get; set; }

        
    }

}
