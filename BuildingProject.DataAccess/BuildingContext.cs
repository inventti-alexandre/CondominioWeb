using BuildingProject.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BuildingProject.DataAccess
{
    public class BuildingContext:DbContext
    {
        public BuildingContext():base("BuildingProjectConnectionString")
        {           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<RoleOption> RoleOption { get; set; }
    }
}
