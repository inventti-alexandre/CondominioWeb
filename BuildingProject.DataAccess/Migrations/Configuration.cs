using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BuildingProject.DataAccess.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<BuildingProject.DataAccess.BuildingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(BuildingProject.DataAccess.BuildingContext context)
        {            
            context.User.AddOrUpdate(p => p.username, new Model.User
            {
                name = "Victor Hugo",
                lastname = "Zambrano Dominguez",
                username = "admin",
                password = "admin",
                active = true,
                dni = "44748194",
                email = "vhzambrano87@gmail.com"
            });
        }

    }
}
