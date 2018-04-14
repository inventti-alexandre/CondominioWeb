using BuildingProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
