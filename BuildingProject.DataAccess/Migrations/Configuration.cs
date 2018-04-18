namespace BuildingProject.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BuildingProject.DataAccess.BuildingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;            
        }

        public static void Seed(BuildingProject.DataAccess.BuildingContext context)
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

            context.Role.AddOrUpdate(p => p.name, new Model.Role
            {
                name = "Administrador",
                active = true
            });


            context.UserRole.AddOrUpdate(p => new { p.userID, p.roleID }, new Model.UserRole { userID = 1, roleID = 1 });

            context.Option.AddOrUpdate(p => p.name, new Model.Option{
                                                        name = "Usuarios",
                                                        active = true
                                                    }
                                                    , new Model.Option
                                                    {
                                                        name = "Roles",
                                                        active = true
                                                    }
                                                    , new Model.Option
                                                    {
                                                        name = "Opciones",
                                                        active = true
                                                    });

            context.RoleOption.AddOrUpdate(p => new { p.roleID, p.optionID }, new Model.RoleOption { roleID = 1, optionID = 1 }
                                                              , new Model.RoleOption { roleID = 1, optionID = 2 }
                                                              );

        }

    }
}
