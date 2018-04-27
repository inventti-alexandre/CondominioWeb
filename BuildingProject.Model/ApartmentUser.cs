namespace BuildingProject.Model
{
    public class ApartmentUser
    {
        public int apartmentUserID { get; set; }
        public int apartmentID { get; set; }
        public int userID { get; set; }
        public virtual Apartment apartment { get; set; }
        public virtual User user { get; set; }
        
    }
}
