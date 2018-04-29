using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace BuildingProject.DataAccess
{
    public class SeedData : DropCreateDatabaseIfModelChanges<BuildingContext>
    {
        protected override void Seed(BuildingContext context)
        {
            context.User.AddOrUpdate(p => p.userID, new Model.User()
            {
                userID=1,
                name = "Victor Hugo",
                lastname = "Zambrano Dominguez",
                username = "vzambrano",
                password = "123",
                active = true,
                dni = "44748194",
                email = "vhzambrano87@gmail.com"
            }
            , new Model.User()
            {
                userID = 2,
                name = "Roger",
                lastname = "Solar",
                username = "rsolar",
                password = "123",
                active = true,
                dni = "12345678",
                email = "rsolar@gmail.com"
            }
            , new Model.User()
            {
                userID = 3,
                name = "Juan Jose",
                lastname = "Chiroque",
                username = "jchiroque",
                password = "123",
                active = true,
                dni = "12345678",
                email = "jchiroque@gmail.com"
            }
            );


            context.Role.AddOrUpdate(p => p.roleID, new Model.Role
            {
                roleID=1,
                name = "God",
                active = true
            }, new Model.Role
            {
                roleID = 2,
                name = "Administrador",
                active = true
            }, new Model.Role
            {
                roleID = 3,
                name = "Vigilante",
                active = true
            }, new Model.Role
            {
                roleID = 4,
                name = "Propietario",
                active = true
            });


            context.UserRole.AddOrUpdate(p => new { p.userRoleID }, new Model.UserRole
            {
                userRoleID=1,
                userID = 1,
                roleID = 1
            }, new Model.UserRole
            {
                userRoleID = 2,
                userID = 2,
                roleID = 1
            }, new Model.UserRole
            {
                userRoleID = 3,
                userID = 3,
                roleID = 1
            }
            );


            context.Option.AddOrUpdate(p => p.optionID, new Model.Option
            {
                optionID=1,
                name = "Usuarios",
                active = true
            }
            , new Model.Option
            {
                optionID = 2,
                name = "Roles",
                active = true
            }
            , new Model.Option
            {
                optionID = 3,
                name = "Opciones",
                active = true
            }
            , new Model.Option
            {
                optionID = 4,
                name = "Condominio",
                active = true
            }
            , new Model.Option
            {
                optionID = 5,
                name = "Seccion",
                active = true
            }, new Model.Option
            {
                optionID = 6,
                name = "Departamento",
                active = true
            });

            context.RoleOption.AddOrUpdate(p => new { p.roleOptionID }, new Model.RoleOption
            {
                roleOptionID = 1,
                roleID = 1,
                optionID = 1
            }
            , new Model.RoleOption
            {
                roleOptionID = 2,
                roleID = 1,
                optionID = 2
            }
            , new Model.RoleOption
            {
                roleOptionID = 3,
                roleID = 1,
                optionID = 3
            }
            , new Model.RoleOption
            {
                roleOptionID = 4,
                roleID = 1,
                optionID = 4
            }
            , new Model.RoleOption
            {
                roleOptionID = 5,
                roleID = 1,
                optionID = 5
            }
            , new Model.RoleOption
            {
                roleOptionID = 6,
                roleID = 1,
                optionID = 6
            });

            context.Building.AddOrUpdate(p => p.buildingID, new Model.Building
            {
                buildingID=1,
                name = "Condominio Orue",
                address = "Av. Domingo Orue",
                addressReference = "A una cuadra de Mi Banco",
                country = "Perú",
                state = "Lima",
                city = "Lima",
                district = "Surquillo",
                apartmentQuantity = 300,
                active = true
            });

            context.Section.AddOrUpdate(p => p.sectionID, new Model.Section
            {
                sectionID=1,
                name = "Torre A",
                buildingID = 1,
                active = true
            }, new Model.Section
            {
                sectionID = 2,
                name = "Torre B",
                buildingID = 1,
                active = true
            }, new Model.Section
            {
                sectionID = 3,
                name = "Torre C",
                buildingID = 1,
                active = true
            }, new Model.Section
            {
                sectionID = 4,
                name = "Torre D",
                buildingID = 1,
                active = true
            });

            context.Apartment.AddOrUpdate(p => p.apartmentID, new Model.Apartment
            {
                apartmentID=1,
                name = "101",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 2,
                name = "102",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 3,
                name = "103",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 4,
                name = "104",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 5,
                name = "201",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 6,
                name = "202",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 7,
                name = "203",
                sectionID = 1,
                active = true
            }, new Model.Apartment
            {
                apartmentID = 8,
                name = "204",
                sectionID = 1,
                active = true
            });

        }
    }
}
