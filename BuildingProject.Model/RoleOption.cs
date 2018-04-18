
namespace BuildingProject.Model
{
    public class RoleOption
    {
        public int roleOptionID { get; set; }
        public int roleID { get; set; }
        public int optionID { get; set; }
        public virtual Role role { get; set; }
        public virtual Option option { get; set; }
    }
}
