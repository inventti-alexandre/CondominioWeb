using BuildingProject.Model;
using System.Collections.Generic;

namespace BuildingProject.ModelView
{
    public class RoleOptionView
    {
        public Role role { get; set; }
        public IList<RoleOption> roleOptionList { get; set; }
        public IList<Option> options { get; set; }
    }
}
