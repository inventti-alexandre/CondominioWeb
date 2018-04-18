namespace BuildingProject.Model
{
    public class UserRole
    {
        public int userRoleID { get; set; }
        public int userID { get; set; }
        public int roleID { get; set; }
        public virtual User user { get; set; }
        public virtual Role role { get; set; }

    }
}
