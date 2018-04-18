using BuildingProject.Model;
using System.Collections.Generic;

namespace BuildingProject.ModelView
{
    public class UserRoleView
    {
        public User user { get; set; }
        public IList<UserRole> userRoleList { get; set; }
        public IList<Role> roles { get; set; }
    }
}
