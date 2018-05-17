using BuildingProject.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BuildingProject.DataAccess
{
    public class BuildingContext:DbContext
    {
        public BuildingContext():base("BuildingProjectConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new SeedData());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BuildingContext>());
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
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<ApartmentUser> ApartmentUser { get; set; }
        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostCategory> PostCategory { get; set; }

    }
}
