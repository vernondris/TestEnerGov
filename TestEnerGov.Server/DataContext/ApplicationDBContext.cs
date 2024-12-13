using EnerGovSolutions.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;

namespace EnerGovSolutions.DataContext
{
    public partial class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) 
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleMasterFile> RoleMasterFile { get; set; }
        public virtual DbSet<Hierarchy> Hierarchy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
